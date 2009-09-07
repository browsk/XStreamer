using System;
using System.Collections.Generic;
using XStreamer.FileSystem.Exception;
using Xunit;

namespace XStreamer.FileSystem.Test
{
    public class VirtualFolderTest
    {
        [Fact]
        public void Test_Returns_Correct_Folder_When_Initialised_With_No_Shares()
        {
            var virtualFolder = new VirtualFolder(new Dictionary<string, string>());

            Assert.Equal(VirtualFolder.FolderRoot, virtualFolder.CurrentFolder);
        }

        [Fact]
        public void Test_Up_Working_Directory_Is_A_Noop_When_When_Initialised_With_No_Shares()
        {
            var virtualFolder = new VirtualFolder(new Dictionary<string, string>());

            virtualFolder.UpWorkingFolder(1);
            Assert.Equal(VirtualFolder.FolderRoot, virtualFolder.CurrentFolder);
        }

        [Fact]
        public void Test_Set_Working_Directory_Throws_Exception_When_Initialised_With_No_Shares()
        {
            var virtualFolder = new VirtualFolder(new Dictionary<string, string>());

            Assert.Throws<FolderDoesNotExistException>(() => virtualFolder.SetWorkingFolder("blah"));
        }

        [Fact]
        public void Test_ShareNameForPath_With_Path_Containing_Only_Share()
        {
            const string path = "/share";

            Assert.Equal("share", VirtualFolder.ShareNameForPath(path));
        }

        [Fact]
        public void Test_ShareNameForPath_With_Path_Containing_Only_Share_With_Trailing_Underscore()
        {
            const string path = "/share/";

            Assert.Equal("share", VirtualFolder.ShareNameForPath(path));
        }

        [Fact]
        public void Test_ShareNameForPath_With_Path()
        {
            const string path = "/share/some/path/here";

            Assert.Equal("share", VirtualFolder.ShareNameForPath(path));
        }

        [Fact]
        public void Test_ShareNameForPath_Throws_Exception_With_null_Path()
        {
            Assert.Throws<ArgumentException>(() => VirtualFolder.ShareNameForPath(null));
        }

        [Fact]
        public void Test_ShareNameForPath_Throws_Exception_With_Empty_Path()
        {
            Assert.Throws<ArgumentException>(() => VirtualFolder.ShareNameForPath(""));
        }

        [Fact]
        public void Test_ShareNameForPath_Throws_Exception_When_Path_Doesnt_Contain_A_Share()
        {
            Assert.Throws<PathHasNoShareException>(() => VirtualFolder.ShareNameForPath("/"));
        }


    }
}
