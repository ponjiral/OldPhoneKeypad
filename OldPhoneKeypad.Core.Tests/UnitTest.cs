namespace OldPhoneKeypad.Core.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {                                                
        }

        [Test]
        public void Test1()
        {
            // "33#"                => "E" 
            // "227*#"              => "B"
            // "4433555 555666#"    => "HELLO"
            // "8 88777444666*664#" => "The digit length should be 1�4." because of 666*66 out off dictionary
            // "8 8877744466664#" => "The digit length should be 1�4." because of 6666 out off dictionary
            // "77 7 777"           => return nothing
            // "77 7 777#"          => "QPR"
            // "123456789999#"      => "&ADGJMPTZ"
            // "123456789 99 999 9999#" => "&ADGJMPTWXYZ"
            // "123456789 99 999 9999 99999#"   => "The digit length should be 1�4." because of 99999 lenght > 4
            // "1234567899999#"     => "The digit length should be 1�4." because of 99999 lenght > 4
            // "qwe#"               => "Invalid character 'q' in input."

            Assert.Pass();
        }
    }
}