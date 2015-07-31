
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ERPInventory.Model.Models
{
    public class Category
    {
        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId{get;set;}

        public string Cat_Title { get; set; }
       

        public int  Cat_NodeDepth{get;set;}

        [ForeignKey("Parent")]
        public int? Cat_ParentId{get;set;}
       

      //  public DateTime Cat_CreateTime{get;set;}

        
        //public DateTime Cat_StampTime { get; set; }
       

        public virtual IEnumerable<Category> Categories{get;set;}
        public  Category Parent { get; set; }
       

    }
}
