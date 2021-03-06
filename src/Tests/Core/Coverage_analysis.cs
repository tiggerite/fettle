﻿using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace Fettle.Tests.Core
{
    class Coverage_analysis_has_been_performed : Contexts.Default
    {
        public Coverage_analysis_has_been_performed()
        {
            Given_an_app_to_be_mutation_tested();

            When_mutation_testing_the_app();
        }

        [Test]
        public void Then_only_tests_that_cover_members_are_run()
        {
            MockTestRunner.Verify(r => r.RunTests(
                    It.IsAny<IEnumerable<string>>(),
                    It.Is<IEnumerable<string>>(tn =>
                        tn.Contains("example.test.one"))));

            MockTestRunner.Verify(r => r.RunTests(
                    It.IsAny<IEnumerable<string>>(),
                    It.Is<IEnumerable<string>>(tn =>
                        !tn.Contains("example.test.one"))),
                Times.Never);

            MockTestRunner.Verify(r => r.RunAllTests(
                    It.IsAny<IEnumerable<string>>()),
                Times.Never);
        }
    }

    class Coverage_analysis_has_not_been_performed : Contexts.Default
    {
        public Coverage_analysis_has_not_been_performed()
        {
            Given_an_app_to_be_mutation_tested();
            Given_coverage_analysis_has_not_been_performed();

            When_mutation_testing_the_app();
        }

        [Test]
        public void Then_all_tests_are_run()
        {
            MockTestRunner.Verify(r => r.RunAllTests(It.IsAny<IEnumerable<string>>()));

            MockTestRunner.Verify(r => r.RunTests(
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<IEnumerable<string>>()),
                Times.Never);
        }
    }
}