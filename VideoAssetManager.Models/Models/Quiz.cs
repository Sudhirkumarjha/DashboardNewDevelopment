using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VideoAssetManager.Models
{
    public class QuestionLevelMaster
    {
        [Key]
        public int QuestionLevelId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string QuestionLevelName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public decimal QuestionLevelMarks { get; set; }  
        [DefaultValue(true)]
        public bool Active { get; set; }
        [DefaultValue(false)]
        public bool IsSoftDelete { get; set; }
        public string Description { get; set; }
        public Guid CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        public Guid ModifyBy { get; set; }
        public DateTime ModifyOn { get; set; }
        [NotMapped]
        public string Marks { get; set; }
    }
    public class TagMaster
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Name { get; set; }
        public bool Active { get; set; }
        [DefaultValue(false)]
        public bool IsSoftDelete { get; set; }
        public string Description { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid ModifyBy { get; set; }
        public DateTime ModifyOn { get; set; }
        [NotMapped]
        public string Ids { get; set; }
    }

}