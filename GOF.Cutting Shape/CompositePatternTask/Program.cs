using CompositePatternTask;

var inputText = new InputText("myInput", "myInputValue");
var labelText = new LabelText("myLabel");
var form = new Form("myForm");
form.AddComponent(inputText);
form.AddComponent(labelText);


var formString = form.ConvertToString();

Console.WriteLine(formString);
Console.ReadLine();
