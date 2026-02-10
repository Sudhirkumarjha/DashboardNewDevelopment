using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace VideoAssetManager.Models
{ 
   public class EventMaster
    {
        [MaxLength(10)]
        public string HostName { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
      
        public Guid ParentId { get; set; }
        [MaxLength(250)]
     
        public string Name { get; set; }
        
        public string Description { get; set; }
        [Required]
        public Guid TopicId { get; set; }
        [Required]
        public Guid InstructorId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [DefaultValue(false)]
        public bool Attendance { get; set; }
        [DefaultValue(false)]
        public bool EventLevelAttendance { get; set; }
        [DefaultValue(true)]
        public bool AllDayEvent { get; set; }
        public Guid AdministratorId { get; set; }
        [DefaultValue(true)]
        public bool IsVirtual { get; set; }
        [Required]
        public string HostId { get; set; }

        public Guid MeetingId { get; set; }

        public string StartUrl { get; set; }

        public string JoiningUrl { get; set; }

        public int AverageRating { get; set; }
    
        public string Keywords { get; set; }
        public Guid CertificateId { get; set; }
        [DefaultValue(true)]

        public DateTime RegistrationEndDate { get; set; }
        public DateTime RegistrationCencelDate { get; set; }
        public int MaxBatchSize { get; set; }
        public int MinBatchSize { get; set; }

        public decimal EventPrice { get; set; }
        public int DiscountPecentage { get; set; }
        public bool Active { get; set; }
        [DefaultValue(false)]
        public bool IsSoftDelete { get; set; }
        [MaxLength(10)]
        public string Status { get; set; }
        [MaxLength(10)]
        public string Visibility { get; set; }
        public Guid CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        public Guid ModifyBy { get; set; }
        public DateTime ModifyOn { get; set; }
        public string ResourceFile { get; set; }
        public Guid? CourseLaunchGuid { get; set; }
    }
    public class TopicMaster
    {
        [MaxLength(10)]
        public string HostName { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]     
        public Guid TopicId { get; set; }
     
        public string TopicName { get; set; }
        public string Description { get; set; }
    }
    public class InstructorMaster
    {
        [MaxLength(10)]
        public string HostName { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid InstructorGuidId { get; set; }
        [Required]
        public int  Type { get; set; }
        [Required]
        public int Rating { get; set; }
        
        public string InstructorBio { get; set; }
    }
    public class TopicInstructorMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid TopicId { get; set; }
        public Guid InstructorId { get; set; }
       
    }

    public class EventInstructorMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        [Required]
        public Guid InstructorId { get; set; }

    }
    public class EventMappingcls
    {
        public Guid EventId { get; set; }
        [Required]
        public Guid InstructorId { get; set; }

    }

    public class InstructorMasterUserDetail
    {
        [MaxLength(10)]
        public string HostName { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid InstructorGuidId { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public int Rating { get; set; }
        public string InstructorBio { get; set; }
        [MaxLength(60)]
        [Required]
        public string Email { get; set; }
    }

    public class GetYourFreePass
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is required.")]            
        public string Mobile { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public short EventType { get; set; }
        public string CountryCode { get; set; }
        public List<OrderDetails> OrderDetail { get; set; }
       //public List<PaymentResponse> PaymentRespons { get; set; }
        public string SourceRef { get; set; }
        public string FestivalDay { get; set; }
        public int Quantity { get; set; }
        public int bulkId { get; set; }
        public decimal Total { get; set; }
    }

    public class OrderDetails
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public char Category { get; set; }
        public string ShowType { get; set; }
    }
    public class PaymentResponse
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public char PaymentStatus { get; set; }
        public string Token { get; set; }
        public string PayerId { get; set; }
        public string PaymentId { get; set; }
    }
}