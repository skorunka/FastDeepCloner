﻿using FastDeepCloner.tests.Entitys;
using FastDeepCloner.tests.Entitys.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FastDeepCloner.tests
{
    [TestClass]
    public class DeepClonerTests
    {

        [TestMethod]
        public void ProxyTest()
        {
            var pUser = DeepCloner.CreateProxyInstance<User>();
            pUser.Name = "testo";
        }

        [TestMethod]
        public void ConvertToInterface()
        {
            var us = new User() { Name = "Alen" };
            IUser pUser = us.ActAsInterface<IUser>();
            string t = pUser.Name;
            pUser.Name = "testo";
        }

        [TestMethod]
        public void ConvertToListInterface()
        {
            var us = new User() { Name = "Alen" };
            var lst = new ObservableCollection<User>() { us };
            var pUser = lst.ActAsInterface<IList<IUser>>();
            var user = pUser.First();
        }


        [TestMethod]
        public void ConvertDynamicToInterface()
        {
            IUser pUser = new { Name = "Mother fuckers", Tal = 15, ps = "sdkjhaskdjh" }.ActAsInterface<IUser>();
            string t = pUser.Name;
            pUser.Name = "testo";
        }

        [TestMethod]
        public void CreateInstance()
        {
            var test1 = DeepCloner.CreateInstance<ParamUsers>();

            var test2 = DeepCloner.CreateInstance<ParamUsers>(new object[] { "test1", "test2", 54 });

            var test3 = DeepCloner.CreateInstance<ParamUsers>(new object[] { "test1", "test2" });
        }



        [TestMethod]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToBeCloned">Desire object to cloned</param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public void Clone()
        {
            var user = new List<User>();
            user.Add(new User() { Name = "Alen" });

            var cloned = FastDeepCloner.DeepCloner.Clone(user);
            cloned.First().Name = "Toma";
            Assert.AreNotEqual(user.First().Name, cloned.First().Name);
        }

        [TestMethod]
        public void CloneTo()
        {
            var user = new User() { Name = "alen toma" };
            var cloneTo = new CloneToTest();

            FastDeepCloner.DeepCloner.CloneTo(user, cloneTo);

            Assert.AreEqual(user.Name, cloneTo.FirstName);
        }

        [TestMethod]
        /// <summary>
        /// Circular refernces Test
        /// </summary>
        /// <param name="objectToBeCloned">Desire object to cloned</param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public void CloneCircularReferences()
        {
            var item = new Circular();
            var cloned = FastDeepCloner.DeepCloner.Clone(item);

            var s = cloned;
        }


        [TestMethod]

        public void DynamicClone()
        {
            dynamic originalAnonnymousObject = new
            {
                prop1 = "p1"
            ,
                prop2 = 10
            };
            dynamic clonedAnonnymousObject = FastDeepCloner.DeepCloner.CloneDynamic(originalAnonnymousObject);
            Assert.AreEqual(clonedAnonnymousObject.prop1, originalAnonnymousObject.prop1);

        }



        [TestMethod]
        public void Convrter()
        {
            var data = DeepCloner.ValueConverter<int?>("120jm"); // null
            var data1 = DeepCloner.ValueConverter<int>("120jm"); // 0
            var data2 = DeepCloner.ValueConverter<int>("120"); // 120
            var data3 = DeepCloner.ValueConverter<decimal>("12,5"); // 12.5
            var data4 = DeepCloner.ValueConverter<decimal?>("552602.25"); // 552602.25
            var data5 = DeepCloner.ValueConverter<DateTime?>("2015-01-01sd"); // null
            var data6 = DeepCloner.ValueConverter<DateTime>("2015-01-01"); // DateTime
            var base64String1 = DeepCloner.ValueConverter<string>(new byte[] { 116, 101, 115, 116 }); // base64string
            var base64String2 = DeepCloner.ValueConverter<byte[]>("dGVzdA=="); // array
        }


        [TestMethod]
        public void CloneCollection()
        {
            var user = new UserCollections();
            user.Add(new User() { Name = "Alen" });
            user.TestValue = "Alen";
            user.us = new User() { Name = "Toma" };
            var cloned = DeepCloner.Clone(user);
            Assert.AreEqual(cloned.us.Name, user.us.Name);
            Assert.AreEqual(cloned.TestValue, "Alen");

        }
    }
}
