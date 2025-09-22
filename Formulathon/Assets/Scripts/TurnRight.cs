public class TurnRight : Command
{
    private PlayerController _controller;

    public TurnRight(PlayerController controller)
    {
        _controller = controller;
    }

    public override void Execute()
    {
        _controller.Turn(PlayerController.Direction.Right);
    }
}
