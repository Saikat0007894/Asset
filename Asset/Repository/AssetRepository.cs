using Asset.Data;
using Asset.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Repository
{
    public class AssetRepository
    {
        private readonly AssetContext _context = null;

        public AssetRepository(AssetContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsset(SystemModel model)
        {
            var newSystem = new SystemTable()
            {
                SystemType = model.SystemType,
                ModelNo = model.ModelNo,
                SerialNo = model.SerialNo,
                Price = model.Price,
                Brand = model.Brand,
                Configaration = model.Configaration,
                Avialable = true

            };
            await _context.System.AddAsync(newSystem);
            await _context.SaveChangesAsync();
            return newSystem.SystemId;

        }

        public async Task<List<EmployeeModel>> Employee()
        {
            var Employees = new List<EmployeeModel>();
            var AllEmployees = await _context.Employee.ToListAsync();
            if (AllEmployees?.Any() == true)
            {
                foreach (var employee in AllEmployees)
                {
                    Employees.Add(new EmployeeModel()
                    {
                        EmpId = employee.EmpId,
                        Name = employee.Name,
                        Depertment = employee.Depertment
                    });
                }
            }
            return Employees;
        }
        public async Task<List<SystemModel>> Asset()
        {
            var Assets = new List<SystemModel>();
            var AllAssets = await _context.System.ToListAsync();
            if (AllAssets?.Any() == true)
            {
                foreach (var asset in AllAssets)
                {
                    Assets.Add(new SystemModel()
                    {
                        SystemType = asset.SystemType,
                        SystemId = asset.SystemId,
                        ModelNo = asset.ModelNo,
                        SerialNo = asset.SerialNo,
                        Avialable = asset.Avialable,
                        Configaration = asset.Configaration,
                        Brand = asset.Brand,
                        Price = asset.Price
                    });
                }
            }
            return Assets;
        }
        public async Task<int> TakingFromSystem(OrderModel orderModel)
        {
            
            var system = await _context.System.FindAsync(orderModel.SystemId);
            var employee = await _context.Employee.FindAsync(orderModel.EmployeeId);
            if (system.Avialable==true)
            {
                //var system = await _context.System.FindAsync(orderModel.EmployeeId);
                var orderRow = new OrderTable()
                {
                    SystemId = orderModel.SystemId,
                    EmployeeId = orderModel.EmployeeId,
                    IssueDate = DateTime.Now,
                    EmpName=employee.Name,
                    SystemType=system.SystemType,
                    ModelNo=system.ModelNo,
                    SerialNo=system.SerialNo
                    
                };

                await _context.Order.AddAsync(orderRow);
                await _context.SaveChangesAsync();
                if (orderRow.OrderId > 0)
                {
                    system.Avialable = false;
                    _context.System.Update(system);
                    await _context.SaveChangesAsync();

                }
                return 1;
            }
            else
            {
                return 0;
            }

        }
        public async Task<int> ReturningFromSystem(OrderModel od)
        {

            var system = await _context.System.FindAsync(od.SystemId);
            var order = _context.Order.FirstOrDefault( x=>x.SystemId== od.SystemId && x.SubmissionDate==null);
            if (system.Avialable == false)
            {
                order.SubmissionDate = DateTime.Now;
                order.Fine = od.Fine;
                _context.Order.Update(order);
                system.Avialable = true;
                _context.System.Update(system);
                await _context.SaveChangesAsync();
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public async Task<int> TakingFromEmployee(OrderModel orderModel)
        {
            var Emp = await _context.Employee.FindAsync(orderModel.EmployeeId);
            var sys = await _context.System.FindAsync(orderModel.SystemId);
            if (sys.Avialable == true)
            {
                var orderRow = new OrderTable()
                {
                    SystemId=sys.SystemId,
                    EmployeeId=Emp.EmpId,
                    IssueDate=DateTime.Now,
                };
                await _context.Order.AddAsync(orderRow);
                await _context.SaveChangesAsync();
                if (orderRow.OrderId > 0)
                {
                    sys.Avialable = false;
                    _context.System.Update(sys);
                    await _context.SaveChangesAsync();

                }
                return 1;
            }
            else
            {
                return 0;
            }

        }
        public async Task<List<OrderModel>> SystemHistory(int systemId)
        {
            var order= new List<OrderModel>();
            var orders =  _context.Order.Where(x => x.SystemId == systemId).ToList();
            if (orders?.Any() == true)
            {
               
                foreach (var or in orders)
                {
                    if (or.SubmissionDate != null)
                    {
                        order.Add(new OrderModel()
                        {
                            EmployeeId = or.EmployeeId,
                            SystemId = or.SystemId,
                            OrderId = or.OrderId,
                            IssueDate = or.IssueDate,
                            SubmissionDate = (DateTime)or.SubmissionDate,
                            Fine=or.Fine,
                            EmpName=or.EmpName,
                            ModelNo=or.ModelNo,
                            SerialNo=or.SerialNo
                        });
                    }
                    else
                    {
                        order.Add(new OrderModel()
                        {
                            EmployeeId = or.EmployeeId,
                            SystemId = or.SystemId,
                            OrderId = or.OrderId,
                            IssueDate = or.IssueDate,
                           EmpName = or.EmpName,
                            ModelNo = or.ModelNo,
                            SerialNo = or.SerialNo
                        });
                    }

                }
            }
            return order;
        }
        public async Task<List<OrderModel>> EmployeeHistory(int employeeId)
        {
            var order = new List<OrderModel>();
            var orders = _context.Order.Where(x => x.EmployeeId == employeeId).ToList();
            if (orders?.Any() == true)
            {
                foreach (var or in orders)
                {
                    if (or.SubmissionDate != null)
                    {
                        order.Add(new OrderModel()
                        {
                            EmployeeId = or.EmployeeId,
                            SystemId = or.SystemId,
                            OrderId = or.OrderId,
                            IssueDate = or.IssueDate,
                            SubmissionDate = (DateTime)or.SubmissionDate,
                            Fine=or.Fine,
                            SystemType=or.SystemType,
                            SerialNo=or.SerialNo,
                            ModelNo=or.ModelNo
                            
                        });
                    }
                    else
                    {
                        order.Add(new OrderModel()
                        {
                            EmployeeId = or.EmployeeId,
                            SystemId = or.SystemId,
                            OrderId = or.OrderId,
                            IssueDate = or.IssueDate,
                            
                        });
                    }
                }
            }
            return order;
        }
        
    }
    }

