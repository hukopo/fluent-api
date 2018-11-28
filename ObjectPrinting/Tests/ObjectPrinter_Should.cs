﻿using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ObjectPrinting.Tests
{
    class ObjectPrinter_Should
    {
        private class PropertyContainer<T>
        {
            public T Content { get; set; }

            public PropertyContainer(T value) => Content = value;
        }

        private class FieldContainer<T>
        {
            public T Content;

            public FieldContainer(T value) => Content = value;
        }

        class TreeNode<T>
        {
            public TreeNode<T> parent { get; set; }

            T Value { get; set; }

            public TreeNode(T value) => Value = value;
        }

        [Test]
        public void Printer_Property_Default_Should()
        {
            var container = new PropertyContainer<int>(10);
            var type = container.GetType();
            var expectedResult = type.Name + Environment.NewLine + "\tContent = 10" + Environment.NewLine;

            var printingConfig = ObjectPrinter.For<PropertyContainer<int>>();
            var actual = printingConfig.PrintToString(container);

            actual.Should().Be(expectedResult);
        }

        [Test]
        public void Printer_Field_Default_Should()
        {
            var container = new FieldContainer<int>(10);
            var type = container.GetType();
            var expectedResult = type.Name + Environment.NewLine + "\tContent = 10" + Environment.NewLine;

            var printingConfig = ObjectPrinter.For<FieldContainer<int>>();
            var actual = printingConfig.PrintToString(container);

            actual.Should().Be(expectedResult);
        }

        [Test]
        public void Printer_Property_ExcludingType_Should()
        {
            var container = new PropertyContainer<int>(10);
            var type = container.GetType();
            var expectedResult = type.Name + Environment.NewLine;

            var printingConfig = ObjectPrinter.For<PropertyContainer<int>>()
                .Excluding<int>();
            var actual = printingConfig.PrintToString(container);

            actual.Should().Be(expectedResult);
        }

        [Test]
        public void Printer_Field_ExcludingType_Should()
        {
            var container = new FieldContainer<int>(10);
            var type = container.GetType();
            var expectedResult = type.Name + Environment.NewLine;

            var printingConfig = ObjectPrinter.For<FieldContainer<int>>()
                .Excluding<int>();
            var actual = printingConfig.PrintToString(container);

            actual.Should().Be(expectedResult);
        }

        [Test]
        public void Printer_ExcludingProperty_Should()
        {
            var container = new PropertyContainer<int>(10);
            var type = container.GetType();
            var expectedResult = type.Name + Environment.NewLine;

            var printingConfig = ObjectPrinter.For<PropertyContainer<int>>()
                .Excluding(o => o.Content);
            var actual = printingConfig.PrintToString(container);

            actual.Should().Be(expectedResult);
        }

        [Test]
        public void Printer_ExcludingField_Should()
        {
            var container = new FieldContainer<int>(10);
            var type = container.GetType();
            var expectedResult = type.Name + Environment.NewLine;

            var printingConfig = ObjectPrinter.For<FieldContainer<int>>()
                .Excluding(o => o.Content);
            var actual = printingConfig.PrintToString(container);

            actual.Should().Be(expectedResult);
        }

        [Test]
        public void Printer_Property_UsingCulture_Should()
        {
            var container = new PropertyContainer<double>(1.1);
            var type = container.GetType();
            var expectedResult = type.Name + Environment.NewLine + "\tContent = 1,10 ₽" + Environment.NewLine;
            var cultureInfo = CultureInfo.CurrentCulture;

            var printingConfig = ObjectPrinter.For<PropertyContainer<double>>()
                .Printing<double>().Using(CultureInfo.CurrentCulture);
            var actual = printingConfig.PrintToString(container);

            actual.Should().Be(expectedResult);
        }

        [Test]
        public void Printer_Field_UsingCulture_Should()
        {
            var container = new FieldContainer<double>(1.1);
            var type = container.GetType();
            var expectedResult = type.Name + Environment.NewLine + "\tContent = 1,10 ₽" + Environment.NewLine;
            var cultureInfo = CultureInfo.CurrentCulture;

            var printingConfig = ObjectPrinter.For<FieldContainer<double>>()
                .Printing<double>().Using(CultureInfo.CurrentCulture);
            var actual = printingConfig.PrintToString(container);

            actual.Should().Be(expectedResult);
        }

        [Test]
        public void Printer_Property_UsingNewTypeSerializer_Should()
        {
            var container = new PropertyContainer<int>(10);
            var type = container.GetType();
            var expectedResult = type.Name + Environment.NewLine + "\tContent = 11" + Environment.NewLine;

            var printingConfig = ObjectPrinter.For<PropertyContainer<int>>()
                .Printing<int>().Using(x => (x + 1).ToString());
            var actual = printingConfig.PrintToString(container);

            actual.Should().Be(expectedResult);
        }

        [Test]
        public void Printer_Field_UsingNewTypeSerializer_Should()
        {
            var container = new FieldContainer<int>(10);
            var type = container.GetType();
            var expectedResult = type.Name + Environment.NewLine + "\tContent = 11" + Environment.NewLine;

            var printingConfig = ObjectPrinter.For<FieldContainer<int>>()
                .Printing<int>().Using(x => (x + 1).ToString());
            var actual = printingConfig.PrintToString(container);

            actual.Should().Be(expectedResult);
        }

        [Test]
        public void Printer_UsingNewPropertySerializer_Should()
        {
            var container = new PropertyContainer<int>(10);
            var type = container.GetType();
            var expectedResult = type.Name + Environment.NewLine + "\tContent = 11" + Environment.NewLine;

            var printingConfig = ObjectPrinter.For<PropertyContainer<int>>()
                .Printing(o => o.Content).Using(x => (x + 1).ToString());
            var actual = printingConfig.PrintToString(container);

            actual.Should().Be(expectedResult);
        }

        [Test]
        public void Printer_UsingNewFieldSerializer_Should()
        {
            var container = new FieldContainer<int>(10);
            var type = container.GetType();
            var expectedResult = type.Name + Environment.NewLine + "\tContent = 11" + Environment.NewLine;

            var printingConfig = ObjectPrinter.For<FieldContainer<int>>()
                .Printing(o => o.Content).Using(x => (x + 1).ToString());
            var actual = printingConfig.PrintToString(container);

            actual.Should().Be(expectedResult);
        }

        [Test]
        public void Printer_Property_UsingTrimm_Should()
        {
            var container = new PropertyContainer<string>("string");
            var type = container.GetType();
            var expectedResult = type.Name + Environment.NewLine + "\tContent = stri" + Environment.NewLine;

            var printingConfig = ObjectPrinter.For<PropertyContainer<string>>()
                .Printing(o => o.Content).TrimmingToLength(4);
            var actual = printingConfig.PrintToString(container);

            actual.Should().Be(expectedResult);
        }

        [Test]
        public void Printer_Field_UsingTrimm_Should()
        {
            var container = new FieldContainer<string>("string");
            var type = container.GetType();
            var expectedResult = type.Name + Environment.NewLine + "\tContent = stri" + Environment.NewLine;

            var printingConfig = ObjectPrinter.For<FieldContainer<string>>()
                .Printing(o => o.Content).TrimmingToLength(4);
            var actual = printingConfig.PrintToString(container);

            actual.Should().Be(expectedResult);
        }

        [Test]
        public void Printer_CollectionArray_Should()
        {
            var container = new PropertyContainer<int[]>(new []{ 1, 2, 3 });
            var type = container.GetType();
            var expectedResult = type.Name + Environment.NewLine + "\tContent = Int32[] { 1 2 3 }" + Environment.NewLine;

            var printingConfig = ObjectPrinter.For<PropertyContainer<int[]>>();
            var actual = printingConfig.PrintToString(container);

            actual.Should().Be(expectedResult);
        }

        [Test]
        public void Printer_CollectionDictionary_Should()
        {
            var dic = new Dictionary<int, string>
            {
                { 1, "1" },
                { 2, "2" }
            };
            var container = new PropertyContainer<Dictionary<int, string>>(dic);
            var type = container.GetType();
            var expectedResult = type.Name + Environment.NewLine + "\tContent = Dictionary`2 { [1, 1] [2, 2] }" + Environment.NewLine;

            var printingConfig = ObjectPrinter.For<PropertyContainer<Dictionary<int, string>>>();
            var actual = printingConfig.PrintToString(container);

            actual.Should().Be(expectedResult);
        }

        [Test]
        public void Printer_CyclicReferences_Should()
        {
            var treeNode1 = new TreeNode<int>(1);
            var treeNode2 = new TreeNode<int>(2) { parent = treeNode1 };
            treeNode1.parent = treeNode2;
            var type = treeNode1.GetType();
            var expectedResult = type.Name + Environment.NewLine + 
                                 "\tparent = " + type.Name + Environment.NewLine + 
                                 "\t\tparent = TreeNode`1(...)" + Environment.NewLine;

            var printingConfig = ObjectPrinter.For<TreeNode<int>>();
            var actual = printingConfig.PrintToString(treeNode1);

            actual.Should().Be(expectedResult);
        }
    }
}