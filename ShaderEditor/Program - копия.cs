void T()
{
    graphControl.CompatibilityStrategy = new TagTypeCompatibility();

    var someNode = new Node("My Title");
    someNode.Location = new Point(500, 100);
    someNode.AddItem(new NodeCheckboxItem("Check 1", true, false) { Tag = 31337 });
    someNode.AddItem(new NodeCheckboxItem("Check 2", true, false) { Tag = 31337 });
    someNode.AddItem(new NodeCheckboxItem("Check 3", true, false) { Tag = 22 });

    graphControl.AddNode(someNode);




    graphControl.ConnectionAdded += new EventHandler<AcceptNodeConnectionEventArgs>(OnConnectionAdded);
    graphControl.ConnectionAdding += new EventHandler<AcceptNodeConnectionEventArgs>(OnConnectionAdding);
    graphControl.ConnectionRemoving += new EventHandler<AcceptNodeConnectionEventArgs>(OnConnectionRemoved);
    graphControl.ShowElementMenu += new EventHandler<AcceptElementLocationEventArgs>(OnShowElementMenu);

    graphControl.Connect(colorItem, check1Item);
}