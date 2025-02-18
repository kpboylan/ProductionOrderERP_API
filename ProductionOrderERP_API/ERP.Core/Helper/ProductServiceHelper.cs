namespace ProductionOrderERP_API.ERP.Core.Helper
{
    public class ProductServiceHelper : IProductServiceHelper
    {
        public bool ProductNameIsValid(string productName)
            => ValidateProduct.ValidProductName(productName);
    }
}
