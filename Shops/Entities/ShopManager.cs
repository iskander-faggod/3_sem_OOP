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

        public IReadOnlyList<Product> ProductsFromManager => ProductsList;
        private List<Product> ProductsList { get; }

        private List<Shop> ShopsList { get; set; }

        public Shop ShopRegistration(Shop shop)
        {
            ShopsList.Add(shop);
            return shop;
        }

        public void ProductRegistration(Product product)
        {
            if (ProductsList.Contains(product))
            {
                throw new ShopException("Product is already register");
            }

            ProductsList.Add(product);
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
            if (!ShopsList.Contains(shop))
            {
                throw new ShopException("Can not find a shop");
            }

            if (productsBase is null)
            {
                throw new ShopException("Invalid data");
            }

            double fullPrice = productsBase.Sum(productPair => productPair.Key.Price * productPair.Value);

            foreach (Shop currentShop in ShopsList)
            {
                currentShop.Transaction(fullPrice);
                foreach ((Product key, int value) in productsBase)
                {
                    currentShop.ProductBase.TryAdd(key, value);
                }
            }
        }

        public void BuyProduct(Shop shop, Person person, Product product, int productCount)
        {
            if (!shop.ProductBase.ContainsKey(product) || !(person.Fund >= product.Price * productCount))
            {
                throw new ShopException("Invalid data");
            }

            shop.ProductBase[product] -= productCount;
            shop.Transaction(product.Price * productCount);
            person.Transaction(product.Price * productCount);
        }

        public void BuyProducts(Shop shop, Person person, Dictionary<Product, int> productsBase)
        {
            double fullPrice = shop.ProductBase.Sum(productPair => productPair.Key.Price * productPair.Value);

            foreach ((Product product, int count) in productsBase)
            {
                if (shop.ProductBase[product] < count || !(person.Fund >= fullPrice))
                {
                    throw new ShopException("Invalid data");
                }

                shop.ProductBase[product] -= count;
                shop.Transaction(fullPrice);
                person.Transaction(fullPrice);
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

        private void ShopBuyProducts(Shop currentShop, Product product, int productCount)
        {
            if (!(currentShop.Fund >= product.Price * productCount))
            {
                throw new ShopException("Shop doesn't have enough money to delivery products");
            }

            currentShop.Transaction(product.Price * productCount);
            currentShop.ProductBase[product] = productCount;

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