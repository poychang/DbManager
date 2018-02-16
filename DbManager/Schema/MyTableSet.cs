using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbManager.Schema
{
    /// <summary>MyTable 資料表模型</summary>
    public class MyTableSet
    {
        /// <summary>識別碼</summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>說明</summary>
        public string Description { get; set; }

        /// <summary>建立日期</summary>
        [Required]
        public DateTime CreateDate { get; set; }
    }
}
