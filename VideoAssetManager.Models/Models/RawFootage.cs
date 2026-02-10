using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoAssetManager.Models
{
    public class VM_RawFootage
    {
        [Key]
        public Guid Id { get; set; } 
        public string RFId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string VideoTitle { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int NoOfCameraUsed { get; set; }
        [DefaultValue(false)]
        public bool IsLivePerformance { get; set; }
        public string Venue { get; set; }
        //public string Country { get; set; }
      
        public DateTime DateOfEvent { get; set; }
        public string EventTitle { get; set; }
        public int Day { get; set; } = 0;
        //public string Stage { get; set; }

        public string Place { get; set; }

        public string DaySession { get; set; }

        public string Description { get; set; }

        public int Lang { get; set; }= 0;

        [DefaultValue(true)]
        public bool Active { get; set; }
        [DefaultValue(false)]
        public bool IsSoftDelete { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid ModifyBy { get; set; }
        public DateTime ModifyOn { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int ProjectId { get; set; }
        [NotMapped]
        public string ProjectCode { get; set; }
        [NotMapped]
        public int HiddenProjectId { get; set; }
        [NotMapped]
        public DateTime HiddenDateOfEvent { get; set; }
        [NotMapped]
        public int HiddenNoOfCameraUsed { get; set; }
        [NotMapped]
        public string ProjectName { get; set; }
      
        public Guid ? StageId { get; set; }
        [NotMapped]
        public Guid HiddenStageId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int CountryId { get; set; }

        [NotMapped]
        public int HiddenCountryId { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int CityId { get; set; }

        [NotMapped]
        public int HiddenCityId { get; set; }
        [NotMapped]
        public string Country { get; set; }
        [NotMapped]
        public string City { get; set; }
    }

    public class VM_EditedVideos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid ExportGuid { get; set; }
        
        public string RFId { get; set; }
        
        public string Title { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int Resolution { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int VideoType { get; set; }
        [DefaultValue(false)]
        public bool IsForRekhtaApp { get; set; }

        [NotMapped]
        public string VideoTypeName { get; set; }
        
        [DefaultValue(true)]
        public bool Active { get; set; }
        [DefaultValue(false)]
        public bool IsSoftDelete { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid ModifyBy { get; set; }
        public DateTime ModifyOn { get; set; }
        
        public string VideoFileName { get; set; }
       
        public string Thumbnail { get; set; }
        [NotMapped] 
        public string ProjectName { get; set; }
        
        public string Language { get; set; }
        public int? CategoryId { get; set; }
        public List<VM_ArtistMapping> Participants { get; set; } = new List<VM_ArtistMapping>();
		public string Duration { get; set; }

        [Required(ErrorMessage = "Size is required")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Please enter a valid numeric value")]
        public string Size { get; set; }

    }

    public class VM_ArtistMapping
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid ExportId { get; set; }
        public string ArtistName { get; set; }
        public char Gender { get; set; }
        public char Type { get; set; }
        public VM_EditedVideos EditedVideo { get; set; }
    }

    public class VM_VideosResolution
    {
        [Key]
        public int Id { get; set; }
        public string Resolution { get; set; }
    }
    public class VM_Project
    {
        [Key]
        public int Id { get; set; }
        //[Required(ErrorMessage = "This field is required.")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "This field is required.")]
                
        public string Code { get; set; }
        [Required(ErrorMessage = "This field is required.")]       
        public int Type { get; set; }
        [DefaultValue(true)]
        public bool Active { get; set; }
        [DefaultValue(false)]
        public bool IsSoftDelete { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid ModifyBy { get; set; }
        public DateTime ModifyOn { get; set; }
        public int? Event { get; set; }
		public int? NumberOfVideos { get; set; }
        public string TotalVideoHours { get; set; }
        public string TotalSpaceOccupied { get; set; }
    }
    public class VM_Stage
    {
        [Key]
        public int Id { get; set; }
        public Guid StageId { get; set; }
        public string Name { get; set; }
    }
    public class VM_StageLookup
    {
        [Key]
        public Guid Id { get; set; }
        public Guid StageId { get; set; }
        public int ProjectId { get; set; }
        public bool IsSoftDelete { get; set; }
    }

    public class VM_CategoryMaster
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
      
        public int DisplayOrder { get; set; }
        public string Description { get; set; }
        [DefaultValue(true)]
        public bool Active { get; set; }
        [DefaultValue(false)]
        public bool IsSoftDelete { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid ModifyBy { get; set; }
        public DateTime ModifyOn { get; set; }
               
    }

    public class VM_City
    {
        [Key]
        public int CityId { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }
    }

    public class ProjectOverview
    {
        
        public long RowId { get; set; }
        public string Name { get; set; }
        public int? NumberOfVideos { get; set; }
        public string TotalVideoHours { get; set; }
        public string TotalSpaceOccupied { get; set; }
    }
    public class FormInputViewModel
    {
        public string LabelText { get; set; }
        public string FieldName { get; set; } 
        public string Value { get; set; }     
        public bool IsRequired { get; set; }  
    }
}