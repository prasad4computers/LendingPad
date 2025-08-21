namespace BusinessEntities
{
    public enum UserTypes
    {
        Admin = 1,
        Employee = 2,
        Customer = 3
    }

    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
}