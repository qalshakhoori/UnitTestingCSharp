using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class StackTests
    {
        Stack<string> stack;

        [SetUp]
        public void Setup()
        {
            stack = new Stack<string>();
        }

        [Test]
        public void Push_WhenObjAdded_ShouldAddToStack()
        {
            stack.Push("Test");

            Assert.That(stack.Count == 1);
        }

        [Test]
        public void Push_WhenNullPassed_ThroughArgumentNullException()
        {
            Assert.That(() => stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Pop_EmptyStack_ShouldThroughInvlaidOperationException()
        {
            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pup_NotEmptyStack_ReturnObjectOnTopOfStack()
        {
            stack.Push("Obj 1");
            stack.Push("Obj 2");
            stack.Push("Obj 3");

            var result = stack.Pop();

            Assert.That(result == "Obj 3");

        }

        [Test]
        public void Pup_NotEmptyStack_RemoveObjectOnTopOfStack()
        {
            stack.Push("Obj 1");
            stack.Push("Obj 2");
            stack.Push("Obj 3");

            stack.Pop();

            Assert.That(stack.Count == 2);
        }

        [Test]
        public void Peek_EmptyStack_ShouldThroughInvlaidOperationException()
        {
            Assert.That(() => stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_NotEmptyStack_ReturnObjectOnTopOfStack()
        {
            stack.Push("Obj 1");
            stack.Push("Obj 2");
            stack.Push("Obj 3");

            var result = stack.Peek();

            Assert.That(result == "Obj 3");
        }

        [Test]
        public void Peek_NotEmptyStack_ShouldNotRemoveObjectOnTopOfStack()
        {
            stack.Push("Obj 1");
            stack.Push("Obj 2");
            stack.Push("Obj 3");

            stack.Peek();

            Assert.That(stack.Count == 3);
        }
    }
}