using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shops.Entities;

namespace Shops.Tests
{
    public class ShopServiceTest
    {
        private ShopManager _manager;

        [SetUp]
        public void Setup()
        {
            _manager = new ShopManager();
        }

        [Test]
        public void FindShopWithMinPrice()
        {
            var shops1 = new Shop( "name1", "adress1", 30000);
            var shops2 = new Shop( "name2", "adress2", 40000);
            var shops3= new Shop( "name3", "adress3", 50000);
            Shop shop1 = _manager.ShopRegistration(shops1);
            Shop shop2 = _manager.ShopRegistration(shops2);
            Shop shop3 = _manager.ShopRegistration(shops3);

            var product1 = new Product("product1", 10);
            var product2 = new Product("product2", 1000); 
            var product3 = new Product("product3", 0);
            var product4 = new Product("product4", 13372281488);

            _manager.DeliveryProduct(shop1, product1, 1);
            _manager.DeliveryProduct(shop1, product2, 1);
            _manager.DeliveryProduct(shop1, product3, 1);
            shop1.ChangePrice(product2, 20);

            product1 = product1.Clone();
            product2 = product2.Clone();
            product3 = product3.Clone();

            _manager.DeliveryProduct(shop2, product1, 3);
            _manager.DeliveryProduct(shop2, product2, 4);
            shop2.ChangePrice(product2, 100);

            product1 = product1.Clone();
            _manager.DeliveryProduct(shop3, product1, 1000);
            shop3.ChangePrice(product1, 5);

            Shop value = _manager.FindShopWithMinPriceProduct(product2);
            Assert.True(value.ProductBase.Keys.Contains(product2));
            Assert.AreEqual(shop1, value);
        }

        [Test]
        public void AddedProductInAShop()
        {
            Shop shops1 = new Shop("shop1", "shop1adress", 30000);
            Shop shop1 = _manager.ShopRegistration(shops1);
            var product1 = new Product("product1", 10);
            var person = new Person("Name", 3000);
            _manager.DeliveryProduct(shop1, product1, 1);
            Assert.True(shop1.ProductBase.Keys.Contains(product1));
        }

        [Test]
        public void ProductRegistration()
        {
            var product = new Product("name", 1000);
            _manager.ProductRegistration(product);
            var productsList = _manager.ProductsFromManager;
            Assert.True(productsList.Contains(product)) ;
        }

        [Test]
        public void ShopDeliveryAndBuyProducts()
        {
            Shop shops1 = new Shop("shop1", "shop1adress", 30000);
            Shop shop1 = _manager.ShopRegistration(shops1);
            var productBase = new Dictionary<Product, int>();
            var product1 = new Product("product1", 11);
            var product2 = new Product("product2", 1); 
            var product3 = new Product("product3", 1);
            var product4 = new Product("product4", 1);
            productBase.Add(product1, 1);
            productBase.Add(product2, 2);
            productBase.Add(product3, 3);
            productBase.Add(product4, 4);
            _manager.DeliveryProducts(shop1, productBase);
            Assert.True(shop1.ProductBase.Keys.Contains(product1));
            Assert.True(shop1.ProductBase.Keys.Contains(product2));
            Assert.True(shop1.ProductBase.Keys.Contains(product3));
            Assert.True(shop1.ProductBase.Keys.Contains(product4));
            Assert.AreEqual(shop1.ProductBase[product1],1);
            Assert.AreEqual(shop1.ProductBase[product2], 2);
            Assert.AreEqual(shop1.ProductBase[product3],3);
            Assert.AreEqual(shop1.ProductBase[product4], 4);

            var person = new Person("Name", 3000);
            _manager.BuyProducts(shop1, person, productBase);
            Assert.True(shop1.ProductBase.Keys.Contains(product1));
            Assert.True(shop1.ProductBase.Keys.Contains(product2));
            Assert.True(shop1.ProductBase.Keys.Contains(product3));
            Assert.True(shop1.ProductBase.Keys.Contains(product4));
        }
    }
}