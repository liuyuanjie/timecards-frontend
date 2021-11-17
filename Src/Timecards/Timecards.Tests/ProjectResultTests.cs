using System;
using System.Collections.Generic;
using System.Linq;
using Timecards.Infrastructure.Model;
using Xunit;

namespace Timecards.Tests
{
    public class ProjectResultTests
    {
        [Fact]
        public void Should_Return_Tree_Include_One_When_Project_Has_One_Top_Item()
        {
            //Arrange
            var projectName = "Project.01";
            var projectResult = new ProjectResult
            {
                Projects = new List<Project>
                {
                    new Project
                    {
                        ProjectId = new Guid(),
                        Name = projectName
                    }
                }
            };

            //Act
            var result = projectResult.GetProjectTree();

            //Assert
            Assert.Single(result);
            Assert.Equal(projectResult.Projects.First().ProjectId, result.First().ProjectId);
            Assert.Equal(70, result.First().Name.Length);
            Assert.Contains(projectName, result.First().Name);
        }

        [Fact]
        public void Should_Return_Tree_Include_Two_When_Project_Has_Two_Items()
        {
            //Arrange
            var firstProjectName = "Project.01";
            var secondProjectName = "Project.02";
            var projectResult = new ProjectResult
            {
                Projects = new List<Project>
                {
                    new Project
                    {
                        ProjectId = new Guid(),
                        Name = firstProjectName
                    },
                    new Project
                    {
                        ProjectId = new Guid(),
                        Name = secondProjectName
                    }
                }
            };

            //Act
            var result = projectResult.GetProjectTree();

            //Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(projectResult.Projects.First().ProjectId, result.First().ProjectId);
            Assert.Equal(70, result.First().Name.Length);
            Assert.Contains(firstProjectName, result.First().Name);
            Assert.Equal(projectResult.Projects.Last().ProjectId, result.Last().ProjectId);
            Assert.Equal(70, result.Last().Name.Length);
            Assert.Contains(secondProjectName, result.Last().Name);
        }
        
        [Fact]
        public void Should_Return_Tree_Include_Two_When_Project_Has_Two_Items_In_Descending_Order()
        {
            //Arrange
            var firstProjectName = "Project.02";
            var secondProjectName = "Project.01";
            var projectResult = new ProjectResult()
            {
                Projects = new List<Project>
                {
                    new Project
                    {
                        ProjectId = new Guid(),
                        Name = firstProjectName
                    },
                    new Project
                    {
                        ProjectId = new Guid(),
                        Name = secondProjectName
                    }
                }
            };

            //Act
            var result = projectResult.GetProjectTree();

            //Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(projectResult.Projects.Last().ProjectId, result.First().ProjectId);
            Assert.Equal(70, result.First().Name.Length);
            Assert.Contains(secondProjectName, result.First().Name);
            Assert.Equal(projectResult.Projects.First().ProjectId, result.Last().ProjectId);
            Assert.Equal(70, result.Last().Name.Length);
            Assert.Contains(firstProjectName, result.Last().Name);
        }

        [Fact]
        public void Should_Return_Tree_With_Correct_Sort_When_Project_Has_One_Parent_Has_Sub_Items()
        {
            //Arrange
            var projectResult = new ProjectResult
            {
                Projects = new List<Project>
                {
                    new Project
                    {
                        ProjectId = new Guid("00000000-0000-0000-0000-00000000000a"),
                        Name = "Parent.1"
                    },
                    new Project
                    {
                        ProjectId = new Guid(),
                        Name = "Parent.2"
                    },
                    new Project
                    {
                        ProjectId = new Guid(),
                        Name = "Parent.3"
                    },
                    new Project
                    {
                        ParentProjectId = new Guid("00000000-0000-0000-0000-00000000000a"),
                        ProjectId = new Guid(),
                        Name = "Parent-1-Sub.2"
                    },
                    new Project
                    {
                        ParentProjectId = new Guid("00000000-0000-0000-0000-00000000000a"),
                        ProjectId = new Guid(),
                        Name = "Parent-1-Sub.1"
                    }
                }
            };

            //Act
            var result = projectResult.GetProjectTree();

            //Assert
            Assert.Equal(5, result.Count);
            Assert.Contains("Parent.1", result[0].Name);
            Assert.Equal(70, result[0].Name.Length);
            Assert.Equal("Parent-1-Sub.1", result[1].Name);
            Assert.Equal("Parent-1-Sub.2", result[2].Name);
            Assert.Contains("Parent.2", result[3].Name);
            Assert.Equal(70, result[3].Name.Length);
            Assert.Contains("Parent.3", result[4].Name);
            Assert.Equal(70, result[4].Name.Length);
        }
        
        [Fact]
        public void Should_Return_Tree_With_Correct_Sort_When_Project_Has_MultipleParent_Has_Sub_Items()
        {
            //Arrange
            var projectResult = new ProjectResult
            {
                Projects = new List<Project>
                {
                    new Project
                    {
                        ProjectId = new Guid("00000000-0000-0000-0000-00000000000a"),
                        Name = "Parent.1"
                    },
                    new Project
                    {
                        ProjectId = new Guid("00000000-0000-0000-0000-00000000000b"),
                        Name = "Parent.3"
                    },
                    new Project
                    {
                        ParentProjectId = new Guid("00000000-0000-0000-0000-00000000000a"),
                        ProjectId = new Guid(),
                        Name = "Parent-1-Sub.2"
                    },
                    new Project
                    {
                        ParentProjectId = new Guid("00000000-0000-0000-0000-00000000000a"),
                        ProjectId = new Guid(),
                        Name = "Parent-1-Sub.1"
                    },
                    new Project
                    {
                        ParentProjectId = new Guid("00000000-0000-0000-0000-00000000000b"),
                        ProjectId = new Guid(),
                        Name = "Parent-3-Sub.2"
                    },
                    new Project
                    {
                        ParentProjectId = new Guid("00000000-0000-0000-0000-00000000000b"),
                        ProjectId = new Guid(),
                        Name = "Parent-3-Sub.1"
                    },
                    new Project
                    {
                        ProjectId = new Guid(),
                        Name = "Parent.2"
                    }
                }
            };

            //Act
            var result = projectResult.GetProjectTree();

            //Assert
            Assert.Equal(7, result.Count);
            Assert.Contains("Parent.1", result[0].Name);
            Assert.Equal(70, result[0].Name.Length);
            Assert.Equal("Parent-1-Sub.1", result[1].Name);
            Assert.Equal("Parent-1-Sub.2", result[2].Name);
            Assert.Contains("Parent.2", result[3].Name);
            Assert.Equal(70, result[3].Name.Length);
            Assert.Contains("Parent.3", result[4].Name);
            Assert.Equal(70, result[4].Name.Length);
            Assert.Equal("Parent-3-Sub.1", result[5].Name);
            Assert.Equal("Parent-3-Sub.2", result[6].Name);
        }
    }
}