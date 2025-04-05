namespace Backend_TechFix.Configurations
{
    public static class AuthorizationPoliciesConfig
    {
        public static void AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                // Role-based policies
                options.AddPolicy("CanViewRolesPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanViewRoles)));
                options.AddPolicy("CanCreateRolePolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanCreateRole)));
                options.AddPolicy("CanUpdateRolePolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanUpdateRole)));
                options.AddPolicy("CanDeleteRolePolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanDeleteRole)));
                options.AddPolicy("CanAssignRoleToUserPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanAssignRoleToUser)));
                options.AddPolicy("CanRemoveRoleFromUserPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanRemoveRoleFromUser)));

                // Permission-based policies
                options.AddPolicy("CanViewPermissionsPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanViewPermissions)));
                options.AddPolicy("CanAssignPermissionsToRolePolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanAssignPermissionsToRole)));
                options.AddPolicy("CanRemovePermissionsFromRolePolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanRemovePermissionsFromRole)));

                // User-based policies
                options.AddPolicy("CanUpdateSuperAdminCredentialsPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanUpdateSuperAdminCredentials)));
                options.AddPolicy("CanCreateUserPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanCreateUser)));
                options.AddPolicy("CanUpdateUserPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanUpdateUser)));
                options.AddPolicy("CanViewUsersPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanViewUsers)));
                options.AddPolicy("CanDeleteUserPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanDeleteUser)));

                // UserType-based policies
                options.AddPolicy("CanViewUserTypePolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanViewRoles)));
                options.AddPolicy("CanCreateUserTypePolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanCreateRole)));
                options.AddPolicy("CanUpdateUserTypePolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanUpdateRole)));
                options.AddPolicy("CanDeleteUserTypePolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanDeleteRole)));

                // Supplier-based policies
                options.AddPolicy("CanViewSuppliersPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanViewSuppliers)));
                options.AddPolicy("CanCreateSupplierPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanCreateSupplier)));
                options.AddPolicy("CanUpdateSupplierPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanUpdateSupplier)));
                options.AddPolicy("CanDeleteSupplierPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanDeleteSupplier)));

                // Category-based policies
                options.AddPolicy("CanViewCategoriesPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanViewCategories)));
                options.AddPolicy("CanCreateCategoryPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanCreateCategory)));
                options.AddPolicy("CanUpdateCategoryPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanUpdateCategory)));
                options.AddPolicy("CanDeleteCategoryPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanDeleteCategory)));

                // Category-based policies
                options.AddPolicy("CanViewBrandsPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanViewBrands)));
                options.AddPolicy("CanCreateBrandPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanCreateBrand)));
                options.AddPolicy("CanUpdateBrandPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanUpdateBrand)));
                options.AddPolicy("CanDeleteBrandPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanDeleteBrand)));

                // Product-based policies
                options.AddPolicy("CanViewProductsPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanViewProducts)));
                options.AddPolicy("CanCreateProductPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanCreateProduct)));
                options.AddPolicy("CanUpdateProductPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanUpdateProduct)));
                options.AddPolicy("CanDeleteProductPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanDeleteProduct)));

                // RFQ-based policies
                options.AddPolicy("CanViewRFQsPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanViewRFQs)));
                options.AddPolicy("CanCreateRFQPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanCreateRFQ)));
                options.AddPolicy("CanUpdateRFQPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanUpdateRFQ)));
                options.AddPolicy("CanDeleteRFQPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanDeleteRFQ)));

                // Quote-based policies
                options.AddPolicy("CanViewQuotesPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanViewQuotes)));
                options.AddPolicy("CanCreateQuotePolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanCreateQuote)));
                options.AddPolicy("CanUpdateQuotePolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanUpdateQuote)));
                options.AddPolicy("CanUpdateQuoteStatusPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanUpdateQuoteStatus)));
                options.AddPolicy("CanDeleteQuotePolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanDeleteQuote)));

                // Order-based policies
                options.AddPolicy("CanViewOrdersPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanViewOrders)));
                options.AddPolicy("CanCreateOrderPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanCreateOrder)));
                options.AddPolicy("CanUpdateOrderStatusPolicy", policy =>
                    policy.Requirements.Add(new RolePermissionRequirement(Permissions.CanUpdateOrderStatus)));

            });
        }
    }
}
