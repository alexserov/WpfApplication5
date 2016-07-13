﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReflectionFramework;
using NUnit.Framework;

namespace ReflectionHelperTests {
    class Class5 {
        public string String { get; set; }
        public void OptionalParamsMethod1(string str = "hello") {
            String = str;
        }
        public void OptionalParamsMethod2(string str = "hello") {
            String = str;
        }
        public void OptionalParamsMethod3(string str) {
            String = str;
        }
    }

    public interface IClass5 {
        void OptionalParamsMethod1(string str = "hello");
        void OptionalParamsMethod2(string str);
        void OptionalParamsMethod3(string str = "hello");
    }

    [TestFixture]
    public class OptionalParametersTest {
        [Test]
        public void OptionalParamsMethod1() {
            var cl5 = new Class5();
            var wcl5 = cl5.Wrap<IClass5>();
            wcl5.OptionalParamsMethod1();
            Assert.AreEqual("hello", cl5.String);
            wcl5.OptionalParamsMethod1("world");
            Assert.AreEqual("world", cl5.String);
        }

        [Test]
        public void OptionalParamsMethod2() {
            var cl5 = new Class5();
            var wcl5 = cl5.Wrap<IClass5>();
            wcl5.OptionalParamsMethod2("hello");
            Assert.AreEqual("hello", cl5.String);
        }
        [Test]
        public void OptionalParamsMethod3() {
            var cl5 = new Class5();
            var wcl5 = cl5.Wrap<IClass5>();
            wcl5.OptionalParamsMethod3();
            Assert.AreEqual("hello", cl5.String);
            wcl5.OptionalParamsMethod3("world");
            Assert.AreEqual("world", cl5.String);
        }
    }
}
