using NUnit.Framework;
using SingletonProject;

namespace FileHandlerTest
{
    public class FilehandlerTest
    {
        [Test]
        public void IsFileHandlerSingleton()
        {
            Assert.That(FileHandler.GetInstance, Is.EqualTo(FileHandler.GetInstance),
                "The two instance is not same! This Filehandler class is not singleton.");
        }
    }
}