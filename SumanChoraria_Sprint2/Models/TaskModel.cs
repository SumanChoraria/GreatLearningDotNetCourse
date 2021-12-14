using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SumanChoraria_Sprint1.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskModel:BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int AssignedToUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedOn { get; set; }
    }
}
