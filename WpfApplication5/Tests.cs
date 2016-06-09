﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpf.Core.Internal;
using NUnit.Framework;

namespace WpfApplication5 {
    public class Class1 {
        private string publicStringProperty;
        public string LastMethod { get; set; }

        public string PublicStringProperty {
            get {
                LastMethod = "get_PublicStringProperty";
                return publicStringProperty;
            }
            set {
                LastMethod = "set_PublicStringProperty";
                publicStringProperty = value;
            }
        }

        public void PublicVoidMethod() {
            LastMethod = "PublicVoidMethod";
        }

        void PrivateVoidMethod() {
            LastMethod = "PrivateVoidMethod";
        }
    }

    public interface IClass1 {
        void PublicVoidMethod();
        void PrivateVoidMethod();
        string PublicStringProperty { get; set; }
    }

    [TestFixture]
    class Tests {
        [Test]
        public void PublicVoidMethodTest() {
            Class1 cl = new Class1();
            var wrapped = cl.Wrap2<IClass1>().Create();
            wrapped.PublicVoidMethod();
            Assert.AreEqual("PublicVoidMethod", cl.LastMethod);
        }
        [Test]
        public void PrivateVoidMethodTest() {
            Class1 cl = new Class1();
            var wrapped = cl.Wrap2<IClass1>()
                .DefineMember(x => x.PrivateVoidMethod())
                .BindingFlags(BindingFlags.NonPublic | BindingFlags.Instance)
                .EndMember()
                .Create();
            wrapped.PrivateVoidMethod();
            Assert.AreEqual("PrivateVoidMethod", cl.LastMethod);
        }
        [Test]
        public void PublicStringPropertyTest() {
            Class1 cl = new Class1();
            var wrapped = cl.Wrap2<IClass1>()
                .Create();
            wrapped.PublicStringProperty = "hello";
            Assert.AreEqual("set_PublicStringProperty", cl.LastMethod);
            Assert.AreEqual("hello", wrapped.PublicStringProperty);
            Assert.AreEqual("get_PublicStringProperty", cl.LastMethod);
        }
    }

    public class Class2 {
        public void _Boolean(ref Boolean value) {
            value = true;
        }

        public void _SByte(ref SByte value) {
            value = -10;
        }

        public void _Byte(ref Byte value) {
            value = 10;
        }

        public void _Char(ref Char value) {
            value = 'z';
        }

        public void _UInt16(ref UInt16 value) {
            value = 10;
        }

        public void _Int16(ref Int16 value) {
            value = -10;
        }

        public void _UInt32(ref UInt32 value) {
            value = 10;
        }

        public void _Int32(ref Int32 value) {
            value = -10;
        }

        public void _UInt64(ref UInt64 value) {
            value = 10;
        }

        public void _Int64(ref Int64 value) {
            value = -10;
        }

        public void _Single(ref Single value) {
            value = 10f;
        }

        public void _Double(ref Double value) {
            value = 10d;
        }

        public void _String(ref String value) {
            value = "z";
        }

        public void _DateTime(ref DateTime value) {
            value = new DateTime(2000, 01, 01);
        }
        public void _Visibility(ref Visibility value) {
            value = Visibility.Hidden;
        }
    }
    public interface IClass2 {
        void _Boolean(ref Boolean value);
        void _SByte(ref SByte value);
        void _Byte(ref Byte value);
        void _Char(ref Char value);
        void _UInt16(ref UInt16 value);
        void _Int16(ref Int16 value);
        void _UInt32(ref UInt32 value);
        void _Int32(ref Int32 value);
        void _UInt64(ref UInt64 value);
        void _Int64(ref Int64 value);
        void _Single(ref Single value);
        void _Double(ref Double value);
        void _String(ref String value);
        void _DateTime(ref DateTime value);
        void _Visibility(ref Visibility value);
    }

    [TestFixture]
    public class RefTests {
        [Test]
        public void _Boolean() {
            //value = true;
            var cl2 = new Class2();
            var wrapped = cl2.Wrap2<IClass2>().Create();
            Boolean value = false;
            wrapped._Boolean(ref value);
            Assert.AreEqual(true, value);
        }

        [Test]
        public void _SByte() {
            //value = -10;
            var cl2 = new Class2();
            var wrapped = cl2.Wrap2<IClass2>().Create();
            SByte value = 0;
            wrapped._SByte(ref value);
            Assert.AreEqual(-10, value);
        }

        [Test]
        public void _Byte() {
            //value = 10;
            var cl2 = new Class2();
            var wrapped = cl2.Wrap2<IClass2>().Create();
            Byte value = 0;
            wrapped._Byte(ref value);
            Assert.AreEqual(10, value);
        }

        [Test]
        public void _Char() {
            //value = 'z';
            var cl2 = new Class2();
            var wrapped = cl2.Wrap2<IClass2>().Create();
            Char value = '0';
            wrapped._Char(ref value);
            Assert.AreEqual('z', value);
        }

        [Test]
        public void _UInt16() {
            //value = 10;
            var cl2 = new Class2();
            var wrapped = cl2.Wrap2<IClass2>().Create();
            UInt16 value = 0;
            wrapped._UInt16(ref value);
            Assert.AreEqual(10, value);
        }

        [Test]
        public void _Int16() {
            //value = -10;
            var cl2 = new Class2();
            var wrapped = cl2.Wrap2<IClass2>().Create();
            Int16 value = 0;
            wrapped._Int16(ref value);
            Assert.AreEqual(-10, value);
        }

        [Test]
        public void _UInt32() {
            //value = 10;
            var cl2 = new Class2();
            var wrapped = cl2.Wrap2<IClass2>().Create();
            UInt32 value = 0;
            wrapped._UInt32(ref value);
            Assert.AreEqual(10, value);
        }

        [Test]
        public void _Int32() {
            //value = -10;
            var cl2 = new Class2();
            var wrapped = cl2.Wrap2<IClass2>().Create();
            Int32 value = 0;
            wrapped._Int32(ref value);
            Assert.AreEqual(-10, value);
        }

        [Test]
        public void _UInt64() {
            //value = 10;
            var cl2 = new Class2();
            var wrapped = cl2.Wrap2<IClass2>().Create();
            UInt64 value = 0;
            wrapped._UInt64(ref value);
            Assert.AreEqual(10, value);
        }

        [Test]
        public void _Int64() {
            //value = -10;
            var cl2 = new Class2();
            var wrapped = cl2.Wrap2<IClass2>().Create();
            Int64 value = 0;
            wrapped._Int64(ref value);
            Assert.AreEqual(-10, value);
        }

        [Test]
        public void _Single() {
            //value = 10f;
            var cl2 = new Class2();
            var wrapped = cl2.Wrap2<IClass2>().Create();
            Single value = 0;
            wrapped._Single(ref value);
            Assert.AreEqual(10f, value);
        }

        [Test]
        public void _Double() {
            //value = 10d;
            var cl2 = new Class2();
            var wrapped = cl2.Wrap2<IClass2>().Create();
            Double value = 0;
            wrapped._Double(ref value);
            Assert.AreEqual(10d, value);
        }

        [Test]
        public void _String() {
            //value = "z";
            var cl2 = new Class2();
            var wrapped = cl2.Wrap2<IClass2>().Create();
            String value = "0";
            wrapped._String(ref value);
            Assert.AreEqual("z", value);
        }

        [Test]
        public void _DateTime() {
            //value = new DateTime(2000, 01, 01);
            var cl2 = new Class2();
            var wrapped = cl2.Wrap2<IClass2>().Create();
            DateTime value = DateTime.Now;
            wrapped._DateTime(ref value);
            Assert.AreEqual(new DateTime(2000, 01, 01), value);
        }

        [Test]
        public void _Visibility() {
            //value = Visibility.Hidden;
            var cl2 = new Class2();
            var wrapped = cl2.Wrap2<IClass2>().Create();
            Visibility value = Visibility.Visible;
            wrapped._Visibility(ref value);
            Assert.AreEqual(Visibility.Hidden, value);
        }
    }

    public class Class3 {
        public string StringString(ref string value) {
            value = "def";
            return "abc";
        }
        public string StringStringVisibility(ref string value1, ref Visibility value2) {
            value1 = "def";
            value2 = Visibility.Hidden;
            return "abc";
        }
    }

    public interface IClass3 {
        string StringString(ref string value);
        string StringStringVisibility(ref string value1, ref Visibility value2);
    }
    [TestFixture]
    public class RefAndReturnTests {
        [Test]
        public void StringStringTest() {
            var cl3 = new Class3();
            var wrapped = cl3.Wrap2<IClass3>().Create();
            var str = "some";
            var result = wrapped.StringString(ref str);
            Assert.AreEqual("abc", result);
            Assert.AreEqual("def", str);
        }
        [Test]
        public void StringStringVisibility() {
            var cl3 = new Class3();
            var wrapped = cl3.Wrap2<IClass3>().Create();
            var str = "some";
            var visib = Visibility.Collapsed;
            var result = wrapped.StringStringVisibility(ref str, ref visib);
            Assert.AreEqual("abc", result);
            Assert.AreEqual("def", str);
            Assert.AreEqual(Visibility.Hidden, visib);
        }
    }    
}