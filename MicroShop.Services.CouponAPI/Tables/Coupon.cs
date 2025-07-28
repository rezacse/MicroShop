using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroShop.Services.CouponAPI.Tables
{
    public class Coupon
    {
        public Coupon()
        {
            // Default constructor for EF Core
        }


        public Coupon(int id, string code, decimal discountAmount, bool isPercentage, decimal minPurchaseAmount
            , decimal maxDiscountAmount, bool onlyForFirstPurchased, int noOfTimeCanBeUsed, int noOfTimeUsed
            , DateTime startOn, DateTime endOn)
        {
            ID = id;
            Code = code;
            DiscountAmount = discountAmount;
            IsDiscountPercentage = isPercentage;
            MinimumPurchaseAmount = minPurchaseAmount;
            MaximumDiscountAmount = maxDiscountAmount;
            OnlyForFirst = onlyForFirstPurchased;
            NoOfTimeCanBeUsed = noOfTimeCanBeUsed;
            NoTimeOfUsed = noOfTimeUsed;
            StartOn = startOn;
            EndOn = endOn;
        }

        [Key]
        public int ID { get; private set; }

        [Required]
        [StringLength(10)]
        public string Code { get; private set; } = string.Empty;

        [Required]
        [Column(TypeName = "DECIMAL(6,2)")]
        public decimal DiscountAmount { get; private set; }

        [Required]
        public bool IsDiscountPercentage { get; private set; } = false;

        [Required]
        [Column(TypeName = "DECIMAL(6,0)")]
        public decimal MinimumPurchaseAmount { get;  private set; }

        [Required]
        [Column(TypeName = "DECIMAL(4,0)")]
        public decimal MaximumDiscountAmount { get;  private set; }

        public bool OnlyForFirst { get;  private set; } = false;
        public int NoOfTimeCanBeUsed { get;  private set; }
        public int NoTimeOfUsed { get;  private set; }

        public DateTime? StartOn { get;  private set; }
        public DateTime? EndOn { get;  private set; }

    }
}
