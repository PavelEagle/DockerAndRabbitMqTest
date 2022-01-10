using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Producer.Models
{
    public class PublishRequest
    {
        [MaxLength(256)]
        [DefaultValue("Test Title")]
        public string Title { get; set; }
        
        [MaxLength(256)]
        [DefaultValue("Test Description")]
        public string Description { get; set; }
    }
}