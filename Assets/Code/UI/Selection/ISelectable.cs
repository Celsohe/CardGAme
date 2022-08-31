namespace Code.UI.Selection
{
	public interface ISelectable
	{
		bool IsSelectable { get; }
		
		void Select();

		void Unselect();
	}
}