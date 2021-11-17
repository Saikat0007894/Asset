using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Data
{
    public class OrderTable
    {
        [Key]
        public int OrderId { get; set; }
        public int SystemId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string EmpName { get; set; }
        public string SystemType { get; set; }
        public int? Fine  { get; set; }
        public string ModelNo { get; set; }
        public string SerialNo { get; set; }
    } 
}
