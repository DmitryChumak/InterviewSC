using System;

namespace Task2
{
    /// <summary>
    /// Represents a geometric triangle.
    /// </summary>
    public readonly struct Triangle
    {
        /// <summary>
        /// Gets the length of side A of the triangle.
        /// </summary>
        public int SideA { get; }

        /// <summary>
        /// Gets the length of side B of the triangle.
        /// </summary>
        public int SideB { get; }

        /// <summary>
        /// Gets the length of side C of the triangle.
        /// </summary>
        public int SideC { get; }

        /// <summary>
        /// Gets the type of the triangle based on its sides (Equilateral, Isosceles, Scalene).
        /// </summary>
        public TriangleType TriangleType => GetTriangleType();

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> class.
        /// The constructor validates the provided side lengths and assigns them to the triangle's sides.
        /// </summary>
        /// <param name="sideA">The length of side A.</param>
        /// <param name="sideB">The length of side B.</param>
        /// <param name="sideC">The length of side C.</param>
        /// <exception cref="ArgumentException">Thrown when the sides are invalid (e.g., negative or do not satisfy triangle inequality).</exception>
        public Triangle(int sideA, int sideB, int sideC)
        {
            ValidateTriangle(sideA, sideB, sideC);

            SideA = sideA;
            SideB = sideB;
            SideC = sideC;
        }

        private TriangleType GetTriangleType()
        {
            if (SideA == SideB && SideB == SideC)
                return TriangleType.Equilateral;

            if (SideA == SideB || SideB == SideC || SideA == SideC)
                return TriangleType.Isosceles;

            return TriangleType.Scalene;
        }

        private static void ValidateTriangle(int sideA, int sideB, int sideC)
        {
            if (!HasValidSides(sideA, sideB, sideC))
                throw new ArgumentException("Triangle sides must be positive numbers.");

            if (!IsTriangleExists(sideA, sideB, sideC))
                throw new ArgumentException("Triangle cannot be formed with these sides.");
        }

        private static bool HasValidSides(int sideA, int sideB, int sideC)
        {
            return sideA > 0 && sideB > 0 && sideC > 0;
        }

        private static bool IsTriangleExists(int sideA, int sideB, int sideC)
        {
            return sideA + sideB > sideC &&
                   sideA + sideC > sideB &&
                   sideB + sideC > sideA;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Triangle sides: {SideA} {SideB} {SideC}. Triangle type is {TriangleType}";
        }
    }
}
