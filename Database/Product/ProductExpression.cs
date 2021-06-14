using Architecture.Domain;
using Architecture.Model;
using System;
using System.Linq.Expressions;

namespace Architecture.Database
{
    public static class ProductExpression
    {
        //public static Expression<Func<User, long>> AuthId => user => user.Auth.Id;

        public static Expression<Func<Product, ProductModel>> Model => product => new ProductModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            DateAdded = product.DateAdded,
            DateModified = product.DateModified,
            UserId= product.UserId
        };

        public static Expression<Func<Product, bool>> Id(long id)
        {
            return product => product.Id == id;
        }
        public static Expression<Func<Product, bool>> UserId(long userId)
        {
            return product => product.User.Id == userId;
        }

    }
}
