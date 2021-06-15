using System;
using Architecture.Database;
using Architecture.Model;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Architecture.Application
{
    public sealed class ProductAuditTrailService : IProductAuditTrailService
    {
        private readonly IProductAuditTrailRepository _productAuditTrailRepository;

        public ProductAuditTrailService
        (
            IProductAuditTrailRepository productAuditTrailRepository
        )
        {
            _productAuditTrailRepository = productAuditTrailRepository;
        }


        public async Task<List<ProductAuditActionModel>> GetAsync(long productId)
        {
            var fields = new List<string> { "Name", "Description", "Price" };
            var list = new List<long> {productId};
            var productAuditTrail = await _productAuditTrailRepository.GetListAsync(list);

            var productHistory = productAuditTrail.Where(x => x.ProductId == productId).ToList();

            var distinctDate = productHistory.GroupBy(x => x.DateAdded).Select(x => x.FirstOrDefault()?.DateAdded).ToList();

            var historyList = new List<ProductAuditActionModel>();
            foreach (var date in distinctDate)
            {
                var productHistoryDate = productHistory.Where(x => x.DateAdded == date).OrderBy(x => x.Row).ToList();
                if (productHistoryDate.Count == 1)
                {
                    var change = GetChanges(new ProductAuditTrailModel(),productHistory.First(), fields);
                    historyList.Add(
                        new ProductAuditActionModel
                        {
                            ActionName = productHistoryDate.FirstOrDefault().ActionName,
                            Action = productHistoryDate.FirstOrDefault().Action,
                            DateAdded = productHistoryDate.FirstOrDefault().DateAdded,
                            Properties = change.ToList()
                        });
                }
                else if (productHistoryDate.Count == 2)
                {
                    var change = GetChanges(productHistory.First(), productHistory.LastOrDefault(), fields);
                    historyList.Add(
                        new ProductAuditActionModel
                        {
                            ActionName = productHistoryDate.FirstOrDefault().ActionName,
                            Action = productHistoryDate.FirstOrDefault().Action,
                            DateAdded = productHistoryDate.FirstOrDefault().DateAdded,
                            Properties = change.ToList()
                        });
                }


            }
            return historyList;
        }

        public static IList<PropertyAuditTrail> GetChanges(object first, object second, IList<string> fieldList)
        {
            var changes = new List<PropertyAuditTrail>();

            var propertiesFirst = GetProperties(first, fieldList);
            var propertiesSecond = GetProperties(second, fieldList);

            for (var i = 0; i < propertiesFirst.Count; i++)
            {
                var propertyFirst = propertiesFirst[i];
                var propertySecond = propertiesSecond[i];

                var valueFirst = propertyFirst.GetValue(first);
                var valueSecond = propertySecond.GetValue(second);

                var equal = AreEqual(valueFirst, valueSecond, propertyFirst.PropertyType);
                if (!equal)
                {
                    changes.Add(new PropertyAuditTrail
                    {
                        Property = propertyFirst.Name,
                        Before = valueFirst != null ? valueFirst.ToString() : string.Empty,
                        After = valueSecond != null ? valueSecond.ToString() : string.Empty
                    });
                }
            }

            return changes;
        }
        private static bool AreEqual(object valueFirst, object valueSecond, Type type)
        {
            var equal = true;

            if (type == typeof(string))
            {
                equal = (valueFirst == null ? string.Empty : valueFirst.ToString().Trim()) == (valueSecond == null ? string.Empty : valueSecond.ToString().Trim());
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                equal = (valueFirst != null && bool.Parse(valueFirst.ToString())) == (valueSecond != null && bool.Parse(valueSecond.ToString()));
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                equal = (valueFirst == null ? 0 : decimal.Parse(valueFirst.ToString())) == (valueSecond == null ? 0 : decimal.Parse(valueSecond.ToString()));
            }
            else if (type == typeof(Guid) || type == typeof(Guid?))
            {
                equal = (valueFirst == null ? Guid.Empty : Guid.Parse(valueFirst.ToString())) == (valueSecond == null ? Guid.Empty : Guid.Parse(valueSecond.ToString()));
            }
            else if (type == typeof(short) || type == typeof(short?))
            {
                equal = (valueFirst == null ? 0 : short.Parse(valueFirst.ToString())) == (valueSecond == null ? 0 : short.Parse(valueSecond.ToString()));
            }
            else if (type == typeof(byte) || type == typeof(byte?))
            {
                equal = (valueFirst == null ? 0 : byte.Parse(valueFirst.ToString())) == (valueSecond == null ? 0 : byte.Parse(valueSecond.ToString()));
            }
            else if (type == typeof(int) || type == typeof(int?))
            {
                equal = (valueFirst == null ? 0 : int.Parse(valueFirst.ToString())) == (valueSecond == null ? 0 : int.Parse(valueSecond.ToString()));
            }
            else if (type == typeof(long) || type == typeof(long?))
            {
                equal = (valueFirst == null ? 0 : long.Parse(valueFirst.ToString())) == (valueSecond == null ? 0 : long.Parse(valueSecond.ToString()));
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                equal = (valueFirst == null ? DateTime.MinValue : DateTime.Parse(valueFirst.ToString())) == (valueSecond == null ? DateTime.MinValue : DateTime.Parse(valueSecond.ToString()));
            }

            return equal;
        }
        private static IList<PropertyInfo> GetProperties(object obj, IList<string> fieldList)
        {
            return obj.GetType().GetProperties().Where(g => fieldList.Contains(g.Name)).ToList();
        }
    }
}
