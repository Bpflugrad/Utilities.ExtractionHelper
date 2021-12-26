using Tests.Interfaces;
using BPflugrad.Utilities.WrapperGenerator.Tests;

namespace Tests
{
	public class SimpleUnmockableClassWrapper : ISimpleUnmockableClassWrapper
	{
		private readonly SuccessfulTests.SimpleUnmockableClass _simpleUnmockableClass = new SuccessfulTests.SimpleUnmockableClass();

		public string ReturnString(string input) => _simpleUnmockableClass.ReturnString(input);
	}
}