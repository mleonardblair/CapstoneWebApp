namespace EcommerceApp.Client.Helpers
{
    public static class RouteConstants
    {
        // General Pages
        public const string AboutUs = "/about-us";
        public const string Gallery = "/gallery";
        public const string ContactUs = "/contact-us";

        // Shop Page
        public const string Shop = "/shop";
        public const string ProductDetails = "/shop/{id}";

        // Admin Routes
        public const string Admin = "/admin";

        // Admin Product CRUD
        public const string AdminProducts = "/admin/products";
        public const string AdminCreateProduct = "/admin/products/create";
        public const string AdminEditProduct = "/admin/products/edit/{id}";
        public const string AdminDeleteProduct = "/admin/products/delete/{id}";

        // Admin Category CRUD
        public const string AdminCategories = "/admin/categories";
        public const string AdminCreateCategory = "/admin/categories/create";
        public const string AdminEditCategory = "/admin/categories/edit/{id}";
        public const string AdminDeleteCategory = "/admin/categories/delete/{id}";

        // Admin User CRUD
        public const string AdminUsers = "/admin/users";
        public const string AdminCreateUser = "/admin/users/create";
        public const string AdminEditUser = "/admin/users/edit/{id}";
        public const string AdminDeleteUser = "/admin/users/delete/{id}";


    }
}
