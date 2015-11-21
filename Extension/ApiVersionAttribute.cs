using System;

namespace Extension
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ApiVersionAttribute : System.Attribute
    {
        private readonly VersionNumber versionNumber;

        private readonly string operationName;

        public ApiVersionAttribute(string theVersionNumber, string theOperatioName)
        {
            this.versionNumber = new VersionNumber(theVersionNumber);
            this.operationName = theOperatioName;
        }

        public VersionNumber VersionNumber
        {
            get
            {
                return this.versionNumber;
            }
        }

        public string OperatioName
        {
            get
            {
                return this.operationName;
            }
        }
    }

    public class VersionNumber : IComparable<VersionNumber>
    {
        private readonly string version;

        public VersionNumber(string theVersion)
        {
            this.version = theVersion;
        }

        public string Version
        {
            get
            {
                return this.version;
            }
        }

        public int CompareTo(VersionNumber other)
        {
            int aResult = 0;
            if (other == null)
            {
                aResult = 1;
            }
            else
            {
                aResult = CompareVersionNumber(this.version, other.Version);
            }

            return aResult;
        }

        public static bool operator <(VersionNumber theVersionNumber1, VersionNumber theVersionNumber2)
        {
            bool aResult = false;

            if (!ReferenceEquals(theVersionNumber1, null)
           && !ReferenceEquals(theVersionNumber2, null))
            {
                aResult = CompareVersionNumber(theVersionNumber1.Version, theVersionNumber2.Version) < 0;
            }
            else if (ReferenceEquals(theVersionNumber1, null))
            {
                aResult = true;
            }

            return aResult;
        }

        public static bool operator >(VersionNumber theVersionNumber1, VersionNumber theVersionNumber2)
        {
            bool aResult = false;

            if (!ReferenceEquals(theVersionNumber1, null)
         && !ReferenceEquals(theVersionNumber2, null))
            {
                aResult = CompareVersionNumber(theVersionNumber1.Version, theVersionNumber2.Version) > 0;
            }
            else if (ReferenceEquals(theVersionNumber2, null))
            {
                aResult = true;
            }

            return aResult;
        }

        public static bool operator ==(VersionNumber theVersionNumber1, VersionNumber theVersionNumber2)
        {
            bool aResult = false;

            if (!ReferenceEquals(theVersionNumber1, null)
         && !ReferenceEquals(theVersionNumber2, null))
            {
                aResult = CompareVersionNumber(theVersionNumber1.Version, theVersionNumber2.Version) == 0;
            }
            else if (ReferenceEquals(theVersionNumber1, null)
                && ReferenceEquals(theVersionNumber2, null))
            {
                aResult = true;
            }

            return aResult;
        }

        public static bool operator !=(VersionNumber theVersionNumber1, VersionNumber theVersionNumber2)
        {
            bool aResult = false;

            if (!ReferenceEquals(theVersionNumber1, null)
         && !ReferenceEquals(theVersionNumber2, null))
            {
                aResult = CompareVersionNumber(theVersionNumber1.Version, theVersionNumber2.Version) != 0;
            }
            else if (!(ReferenceEquals(theVersionNumber1, null)
                && ReferenceEquals(theVersionNumber2, null)))
            {
                aResult = true;
            }

            return aResult;
        }

        public override bool Equals(object obj)
        {
            bool aResult = false;
            VersionNumber aOtherVersionNumber = obj as VersionNumber;
            if (aOtherVersionNumber != null
           && this == aOtherVersionNumber)
            {
                aResult = true;
            }

            return aResult;
        }

        public override int GetHashCode()
        {
            return this.version.GetHashCode();
        }

        public static int CompareVersionNumber(string theVersionNumber, string theOtherVersionNumber)
        {
            int aResult = 0;
            if (string.Compare(theVersionNumber, theOtherVersionNumber, StringComparison.OrdinalIgnoreCase) != 0)
            {
                if (string.IsNullOrEmpty(theOtherVersionNumber))
                {
                    aResult = 1;
                }
                else if (string.IsNullOrEmpty(theVersionNumber))
                {
                    aResult = -1;
                }
                else
                {
                    string[] aVersionNumberArray = theVersionNumber.Split('.');
                    string[] aOtherVersionNumberArray = theOtherVersionNumber.Split('.');

                    aResult = aVersionNumberArray.Length.CompareTo(aOtherVersionNumberArray.Length);

                    for (int i = 0; i < aVersionNumberArray.Length && i < aOtherVersionNumberArray.Length; i++)
                    {
                        if (aVersionNumberArray[i] == aOtherVersionNumberArray[i])
                        {
                            continue;
                        }
                        else
                        {
                            int aVersionNumberArrayMemberNumber = 0;
                            int aOtherVersionNumberArrayMemberNumber = 0;

                            if (int.TryParse(aVersionNumberArray[i], out aVersionNumberArrayMemberNumber)
                           && int.TryParse(aOtherVersionNumberArray[i], out aOtherVersionNumberArrayMemberNumber))
                            {
                                aResult = aVersionNumberArrayMemberNumber.CompareTo(
                                    aOtherVersionNumberArrayMemberNumber);
                            }
                            else
                            {
                                if (aVersionNumberArrayMemberNumber > 0)
                                {
                                    aResult = 1;
                                }
                                else if (aOtherVersionNumberArrayMemberNumber > 0)
                                {
                                    aResult = -1;
                                }
                                else
                                {
                                    aResult = string.Compare(
                                        aVersionNumberArray[i],
                                        aOtherVersionNumberArray[i],
                                        StringComparison.OrdinalIgnoreCase);
                                }
                            }

                            break;
                        }
                    }
                }
            }

            return aResult;
        }
    }
}
