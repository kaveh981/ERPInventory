
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ERPInventory.Model.Models
{
    public class Category
    {
        // guid works for auto increment
        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CategoryId { get; set; }

        public string Cat_Title { get; set; }


        public int Cat_NodeDepth { get; set; }

        //should be the name of navigation property
        [ForeignKey("Parent")]
        public Guid? Cat_ParentId { get; set; }


        public DateTime Cat_CreateTime { get; set; }

        //int will be set to zero if it is not nullable and we don't set a value to it
        public int Cat_Priority { get; set; }
        public DateTime? Cat_StampTime { get; set; }


        public virtual IEnumerable<Category> Categories { get; set; }
        public Category Parent { get; set; }


    }
}
