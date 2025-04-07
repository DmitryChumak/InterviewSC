using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task2;

namespace Tests
{
    [TestClass]
    public class Task2Tests
    {
        [TestMethod]
        public void Triangle_ValidSides_ReturnsCorrectTriangleType()
        {
            // Arrange
            var triangle = new Triangle(3, 4, 5);

            // Act
            var triangleType = triangle.TriangleType;

            // Assert
            Assert.AreEqual(TriangleType.Scalene, triangleType);
        }

        [TestMethod]
        public void Triangle_EquilateralSides_ReturnsEquilateral()
        {
            // Arrange
            var triangle = new Triangle(5, 5, 5);

            // Act
            var triangleType = triangle.TriangleType;

            // Assert
            Assert.AreEqual(TriangleType.Equilateral, triangleType);
        }

        [TestMethod]
        public void Triangle_IsoscelesSides_ReturnsIsosceles()
        {
            // Arrange
            var triangle = new Triangle(5, 5, 8);

            // Act
            var triangleType = triangle.TriangleType;

            // Assert
            Assert.AreEqual(TriangleType.Isosceles, triangleType);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Triangle_InvalidSides_ThrowsArgumentException()
        {
            // Arrange & Act
            var triangle = new Triangle(1, 2, 10); // This will throw an exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Triangle_NegativeSide_ThrowsArgumentException()
        {
            // Arrange & Act
            var triangle = new Triangle(-1, 5, 5); // This will throw an exception
        }

        [TestMethod]
        public void Triangle_ValidSides_ReturnsToStringWithCorrectInfo()
        {
            // Arrange
            var triangle = new Triangle(3, 4, 5);

            // Act
            var result = triangle.ToString();

            // Assert
            Assert.AreEqual("Triangle sides: 3 4 5. Triangle type is Scalene", result);
        }
    }
}
