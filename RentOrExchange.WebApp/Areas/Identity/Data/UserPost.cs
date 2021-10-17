
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

        [Required]
        [DisplayName("Title")]
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [Column(TypeName = "bit")]
        public bool IsApproved { get; set; }

        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }

        public int PostType { get; set; }

        [DisplayName("Postal Code")]
        [Required]
        public string PostalCode { get; set; }

        public double Price { get; set; }

#nullable enable
        public string? Address { get; set; }
        //public string? ModifiedBy { get; set; }

        ////[DisplayFormat(DataFormatString = "{0:dd-MMM hh:mm tt}")]
        //public DateTime? ModifiedOn { get; set; }
    }
}