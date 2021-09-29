using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Shops.Tools;

namespace Shops.Entities
{
    public class ShopManager
    {
        private readonly List<Product> _productsList;
        private readonly List<Shop> _shopsList;
        public ShopManager()
        {
            _shopsList = new List<Shop>();
            _productsList = new List<Product>();
        }

        public IReadOnlyList<Product> Products => _productsList;
        public Shop ShopRegistration(Shop shop)
        {
            _shopsList.Add(shop);
            return shop;
        }

        public void ProductRegistration(Product product)
        {
            if (_productsList.Contains(product))
            {
                throw new ShopException("Product is already register");
            }

            _productsList.Add(product);
        }

        public void DeliveryProduct(Shop shop, Product product, int productCount)
        {
            if (productCount < 1)
            {
                throw new ShopException($"Invalid productCount - {productCount}");
            }

            Shop currentShop = _shopsList.FirstOrDefault(foundedShop => Equals(foundedShop, shop));
            ShopBuyProducts(currentShop, product, productCount);
        }

        public void DeliveryProducts(Shop shop, Dictionary<Product, int> productsBase)
        {
            if (!_shopsList.Contains(shop))
            {
                throw new ShopException("Can not find a shop");
            }

            if (productsBase is null)
            {
                throw new ShopException("Invalid data");
            }

            double fullPrice = productsBase.Sum(productPair => productPair.Key.Price * productPair.Value);

            foreach (Shop currentShop in _shopsList)
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

            Product shopProduct = shop.FindShopProduct(product);

            shop.ProductBase[shopProduct] -= productCount;
            shop.Transaction(shopProduct.Price * productCount);
            person.Transaction(shopProduct.Price * productCount);
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
            return _shopsList
                .Where(shop => shop.HasProduct(product))
                .OrderBy(shop => shop.ProductBase.Keys
                    .Min(currentProduct => currentProduct.Price))
                .FirstOrDefault();
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