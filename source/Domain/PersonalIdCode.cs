namespace EventNet.Domain
{
    public sealed class PersonalIdCode : IEquatable<PersonalIdCode>
    {
        public string Value { get; }

        public PersonalIdCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Personal ID code cannot be empty.", nameof(value));
            if (value.Length != 11 || !value.All(char.IsDigit))
                throw new ArgumentException("Personal ID code must be 11 digits long.", nameof(value));
            if (!IsValidChecksum(value))
                throw new ArgumentException("Personal ID code is invalid according to checksum calculation.", nameof(value));

            Value = value;
        }

        private static bool IsValidChecksum(string personalIdCode)
        {
            int[] firstLevelWeights = [1, 2, 3, 4, 5, 6, 7, 8, 9, 1];
            int[] secondLevelWeights = [3, 4, 5, 6, 7, 8, 9, 1, 2, 3];

            int checksum = 0;
            for (int i = 0; i < 10; i++)
            {
                checksum += (personalIdCode[i] - '0') * firstLevelWeights[i];
            }
            int remainder = checksum % 11;
            if (remainder != 10)
                return remainder == (personalIdCode[10] - '0');

            checksum = 0;
            for (int i = 0; i < 10; i++)
            {
                checksum += (personalIdCode[i] - '0') * secondLevelWeights[i];
            }
            remainder = checksum % 11;
            if (remainder == 10)
                remainder = 0;

            return remainder == (personalIdCode[10] - '0');
        }

        // Value objects should implement equality based on their value
        public override bool Equals(object obj)
        {
            return Equals(obj as PersonalIdCode);
        }

        public bool Equals(PersonalIdCode other)
        {
            if (other is null)
                return false;
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString() => Value;
    }
}
