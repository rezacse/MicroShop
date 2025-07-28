namespace MicroShop.Services.CouponAPI.Models
{
    public class CouponListItem(int id, string code, decimal discountAmount, bool isDiscountPercentage, decimal minimumPurchaseAmount, decimal maximumDiscountAmount, bool onlyForFirst, int noOfTimeCanBeUsed, int noTimeOfUsed, DateTime? startOn, DateTime? endOn)
    {
        public int ID { get; private set; } = id;

        public string Code { get; private set; } = code;

        public decimal DiscountAmount { get; private set; } = discountAmount;

        public bool IsDiscountPercentage { get; private set; } = isDiscountPercentage;

        public decimal MinimumPurchaseAmount { get; private set; } = minimumPurchaseAmount;

        public decimal MaximumDiscountAmount { get; private set; } = maximumDiscountAmount;

        public bool OnlyForFirst { get; private set; } = onlyForFirst;
        public int NoOfTimeCanBeUsed { get; private set; } = noOfTimeCanBeUsed;
        public int NoTimeOfUsed { get; private set; } = noTimeOfUsed;

        public DateTime? StartOn { get; private set; } = startOn;
        public DateTime? EndOn { get; private set; } = endOn;
    }
}
