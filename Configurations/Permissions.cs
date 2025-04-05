namespace Backend_TechFix.Configurations
{
    public static class Permissions
    {
        // Role-related permissions
        public const string CanViewRoles = "CanViewRoles";
        public const string CanCreateRole = "CanCreateRole";
        public const string CanUpdateRole = "CanUpdateRole";
        public const string CanDeleteRole = "CanDeleteRole";
        public const string CanAssignRoleToUser = "CanAssignRoleToUser";
        public const string CanRemoveRoleFromUser = "CanRemoveRoleFromUser";

        // Permission-related permissions
        public const string CanViewPermissions = "CanViewPermissions";
        public const string CanAssignPermissionsToRole = "CanAssignPermissionsToRole";
        public const string CanRemovePermissionsFromRole = "CanRemovePermissionsFromRole";

        // User-related permissions
        public const string CanUpdateSuperAdminCredentials = "CanUpdateSuperAdminCredentials";
        public const string CanCreateUser = "CanCreateUser";
        public const string CanUpdateUser = "CanUpdateUser";
        public const string CanViewUsers = "CanViewUsers";
        public const string CanDeleteUser = "CanDeleteUser";

        // UserType-related permissions
        public const string CanViewUserTypes = "CanViewUserTypes";
        public const string CanCreateUserTypes = "CanCreateUserTypes";
        public const string CanUpdateUserTypes = "CanUpdateUserTypes";
        public const string CanDeleteUserTypes = "CanDeleteUserTypes";

        // Supplier-related permissions
        public const string CanViewSuppliers = "CanViewSuppliers";
        public const string CanCreateSupplier = "CanCreateSupplier";
        public const string CanUpdateSupplier = "CanUpdateSupplier";
        public const string CanDeleteSupplier = "CanDeleteSupplier";

        // Category-related permissions
        public const string CanViewCategories = "CanViewCategories";
        public const string CanCreateCategory = "CanCreateCategory";
        public const string CanUpdateCategory = "CanUpdateCategory";
        public const string CanDeleteCategory = "CanDeleteCategory";

        // Brand-related permissions
        public const string CanViewBrands = "CanViewBrands";
        public const string CanCreateBrand = "CanCreateBrand";
        public const string CanUpdateBrand = "CanUpdateBrand";
        public const string CanDeleteBrand = "CanDeleteBrand";

        // Product-related permissions
        public const string CanViewProducts = "CanViewProducts";
        public const string CanCreateProduct = "CanCreateProduct";
        public const string CanUpdateProduct = "CanUpdateProduct";
        public const string CanDeleteProduct = "CanDeleteProduct";

        // RFQ-related permissions
        public const string CanViewRFQs = "CanViewRFQs";
        public const string CanCreateRFQ = "CanCreateRFQ";
        public const string CanUpdateRFQ = "CanUpdateRFQ";
        public const string CanDeleteRFQ = "CanDeleteRFQ";

        // Quote-related permissions
        public const string CanViewQuotes = "CanViewQuotes";
        public const string CanCreateQuote = "CanCreateQuote";
        public const string CanUpdateQuote = "CanUpdateQuote";
        public const string CanUpdateQuoteStatus = "CanUpdateQuoteStatus";
        public const string CanDeleteQuote = "CanDeleteQuote";

        // Order-related permissions
        public const string CanViewOrders = "CanViewOrders";
        public const string CanCreateOrder = "CanCreateOrder";
        public const string CanUpdateOrderStatus = "CanUpdateOrderStatus";
        public static List<string> AllPermissions => new List<string>
        {
            CanViewRoles,
            CanCreateRole,
            CanUpdateRole,
            CanDeleteRole,
            CanAssignRoleToUser,
            CanRemoveRoleFromUser,
            CanViewPermissions,
            CanAssignPermissionsToRole,
            CanRemovePermissionsFromRole,
            CanUpdateSuperAdminCredentials,
            CanCreateUser,
            CanUpdateUser,
            CanViewUsers,
            CanDeleteUser,
            CanViewUserTypes,
            CanCreateUserTypes,
            CanUpdateUserTypes,
            CanDeleteUserTypes,
            CanViewSuppliers,
            CanCreateSupplier,
            CanUpdateSupplier,
            CanDeleteSupplier,
            CanViewCategories,
            CanCreateCategory,
            CanUpdateCategory,
            CanDeleteCategory,
            CanViewBrands,
            CanCreateBrand,
            CanUpdateBrand,
            CanDeleteBrand,
            CanViewProducts,
            CanCreateProduct,
            CanUpdateProduct,
            CanDeleteProduct,
            CanViewRFQs,
            CanCreateRFQ,
            CanUpdateRFQ,
            CanDeleteRFQ,
            CanViewQuotes,
            CanCreateQuote,
            CanUpdateQuote,
            CanUpdateQuoteStatus,
            CanDeleteQuote,
            CanViewOrders,
            CanCreateOrder,
            CanUpdateOrderStatus
        };
    }
}
