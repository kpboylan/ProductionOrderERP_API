namespace ProductionOrderERP_API.ERP.Core.Helper
{
    public static class ValidateProduct
    {
        public static bool ValidProductName(string productName)
        {
            bool isValid = false;

            if (productName.Length > 2 && productName.Length < 256)
            {
                isValid = true;
            }

            return isValid;
        }
    }
}
