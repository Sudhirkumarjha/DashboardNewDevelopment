using System.ComponentModel.DataAnnotations;

namespace VideoAssetManager.Models
{
    public class GlobalSetting 
    {
        [Key]
        public int Id { get; set; }
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
    }
}
