
namespace RentOrExchange.WebApp.Areas.Identity.Data
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserPost
    {
        [Column(TypeName = "int")]
        [DisplayName("Id")]
        public int UserPostId { get; set; }

        [DisplayName("Title")]
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }

        [DisplayName("Requirement description")]
        public string Description { get; set; }

        [Column(TypeName = "bit")]
        public bool IsApproved { get; set; }

        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }

        public int PostType { get; set; }

#nullable enable
        public string? ModifiedBy { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd-MMM hh:mm tt}")]
        public DateTime? ModifiedOn { get; set; }
    }
}