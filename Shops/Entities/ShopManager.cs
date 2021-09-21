using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Shops.Tools;

namespace Shops.Entities
{
    public class ShopManager
    {
        public ShopManager()
        {
            ShopsList = new List<Shop>();
            ProductsList = new List<Product>();
        }

        private List<Shop> ShopsList { get; set; }
        private List<Product> ProductsList { get; }
        private int ShopId { get; set; } = 0;

        public Shop CreateShop(string name, string address, double money)
        {
            ShopId++;
            var currentShop = new Shop(ShopId, name, address, money);
            ShopsList.Add(currentShop);
            return currentShop;
        }

        public void ProductRegistration(string productName, double productPrice, int productCount)
        {
            var product = new Product(productName, productPrice);
            if (!ProductsList.Contains(product))
            {
                ProductsList.Add(product);
            }
            else
            {
                throw new ShopException("Product is already register");
            }
        }

        public void DeliveryProduct(Shop shop, Product product, int productCount)
        {
            if (productCount < 1)
            {
                throw new ShopException($"Invalid productCount - {productCount}");
            }

            Shop currentShop = ShopsList.FirstOrDefault(foundedShop => Equals(foundedShop, shop));
            ShopBuyProducts(currentShop, product, productCount);
        }

        public void DeliveryProducts(Shop shop, Dictionary<Product, int> productsBase)
        {
            if (productsBase == null)
            {
                throw new ShopException("Invalid data");
            }

            double fullPrice = productsBase.Sum(productPair => productPair.Key.Price * productPair.Value);

            foreach (Shop currentShop in ShopsList)
            {
                if (Equals(currentShop, shop))
                {
                    currentShop.Transaction(fullPrice);
                    foreach ((Product key, int value) in productsBase)
                    {
                        currentShop.ProductBase.Add(key, value);
                    }
                }
                else
                {
                    throw new ShopException("Can not find a shop");
                }
            }
        }

        public void BuyProduct(Shop shop, Person person, Product product, int productCount)
        {
            if (shop.ProductBase.ContainsKey(product) && person.Fund >= product.Price * productCount)
            {
                shop.ProductBase[product] -= productCount;
                shop.Transaction(product.Price * productCount);
                person.Transaction(product.Price * productCount);
            }
            else
            {
                throw new ShopException("Invalid data");
            }
        }

        public void BuyProducts(Shop shop, Person person, Dictionary<Product, int> productsBase)
        {
            double fullPrice = shop.ProductBase.Sum(productPair => productPair.Key.Price * productPair.Value);

            foreach ((Product product, int count) in productsBase)
            {
                if (shop.ProductBase[product] >= count && person.Fund >= fullPrice)
                {
                    shop.ProductBase[product] -= count;
                    shop.Transaction(fullPrice);
                    person.Transaction(fullPrice);
                    person.Card.Add(product);
                }
                else
                {
                    throw new ShopException("Invalid data");
                }
            }
        }

        public Shop FindShopWithMinPriceProduct(Product product)
        {
            try
            {
                return ShopsList
                    .Where(shop => shop.ProductBase.Keys
                        .Contains(product))
                    .ToDictionary(
                        key => key,
                        value => value.ProductBase.Keys
                            .FirstOrDefault(currentProduct => Equals(currentProduct, product)))
                    .Aggregate((current, next) =>
                        current.Value.Price < next.Value.Price ? current : next).Key;
            }
            catch (Exception error)
            {
                throw new ShopException("Shop not found", error);
            }
        }

        public List<Product> GetProductsList()
        {
            return ProductsList;
        }

        private void ShopBuyProducts(Shop currentShop, Product product, int productCount)
        {
            if (currentShop.Fund >= product.Price * productCount)
            {
                currentShop.Transaction(product.Price * productCount);
                currentShop.ProductBase[product] = productCount;
            }
            else
            {
                throw new ShopException("Shop doesn't have enough money to delivery products");
            }

            if (currentShop.ProductBase.Keys.All(currentProduct => Equals(currentProduct, product)) &&
                currentShop.ProductBase.Any())
            {
                currentShop.ProductBase[product] += productCount;
            }
            else
            {
                currentShop.ProductBase[product] = productCount;
            }
        }
    }
}