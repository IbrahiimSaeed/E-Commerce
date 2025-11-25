using Domain.Entites.OrderModule;

namespace Services.Specifications
{
    internal class OrderWithIncludeSpecifications : BaseSpecifications<Order,Guid>
    {
        //Get by Id
        //Cretria ==> id == o.id
        //Includes ==> (DeliveryMethod , OrderItems)
        public OrderWithIncludeSpecifications(Guid id) : base(o => o.Id == id)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.OrderItems);
        }
        //Get All Orders By Email
        //Cretria ==> email == o.email
        //Includes ==> (DeliveryMethod , OrderItems)
        public OrderWithIncludeSpecifications(string userEmail) : base(o => o.UserEmail == userEmail)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.OrderItems);
            AddOrderBy(o => o.OrderDate);
        }
    }
}
