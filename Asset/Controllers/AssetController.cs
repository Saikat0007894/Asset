using Asset.Data;
using Asset.Models;
using Asset.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Controllers
{
    public class AssetController : Controller
    {
        private readonly AssetRepository _assetRepository = null;
        public AssetController(AssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }
        public IActionResult IntroPage()
        {
            return View();
        }
        public async Task<ViewResult> TakingFromSystemAsync(int systemId)
        {
            var data = new OrderModel()
            {
                SystemId = systemId,
                Employees = await _assetRepository.Employee()
            };
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> TakingFromSystem( OrderModel orderModel)
        {
            if (ModelState.IsValid)
            {
                await _assetRepository.TakingFromSystem(orderModel);
            }
            
            return Redirect("/Asset/Asset"); 
        }
        public async Task<ViewResult> TakingFromEmployeeAsync(int employeeId)
        {
            var data = new OrderModel()
            {
                EmployeeId = employeeId,
                Systems = await _assetRepository.Asset()
            };
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> TakingFromEmployee(OrderModel orderModel)
        {
            if (ModelState.IsValid)
            {
                await _assetRepository.TakingFromSystem(orderModel);
                
            }
          
                return Redirect("/Asset/Asset");
            
        }

        public IActionResult AddAsset()
        {
            return View();
        }

        [HttpPost]       
        public async Task<IActionResult> AddAsset(SystemModel systemTable)
        {
            var id = await _assetRepository.AddAsset(systemTable);

            return Redirect("/Asset/Asset"); 
        }
        
        public async Task<ViewResult> Employee()
        {
            var data = await _assetRepository.Employee();
            return View(data);
        }
        public async Task<ViewResult> Asset()
        {
            var data = await _assetRepository.Asset();
            return View(data);
        }
        public IActionResult ReturningPageFromSystem(int systemId, string systemType)
        {
            var data = new OrderModel
            {
                SystemId = systemId,
                SystemType = systemType
            };
            return View(data);
        }
        public async Task<IActionResult> ReturningFromSystem(OrderModel od)
        {
            await _assetRepository.ReturningFromSystem(od);

            return Redirect("/Asset/Asset" );
        }
        public async Task<ViewResult> SystemHistory(int systemId)
        {
            
            var data= await _assetRepository.SystemHistory(systemId);
            

            return View(data);
        }
        public async Task<ViewResult> EmployeeHistory(int EmployeeId)
        {
            var data = await _assetRepository.EmployeeHistory(EmployeeId);
            return View(data);
        }
        public async Task<ViewResult>ReturningPage (int SystemId)
        {
            var data = await _assetRepository.EmployeeHistory(SystemId);
            return View(data);
        }

    }
}
