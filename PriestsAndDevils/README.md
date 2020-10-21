博客地址：https://blog.csdn.net/qq_40338696/article/details/108942569

其中Models文件中是放了 SSDirector类， ISceneController接口，IUserAction接口，还有角色类（角色，岸，船），角色类包括了生成的方法（名字，状态等属性），行为方法（怎么移动，上下船，上下岸等）。

Move文件是定义了船在水上的移动方法。

然后是Click是定义了用户可以点击的对象。

Controller文件则是控制游戏，先是拿到单例，生成GUI，生成所有对象资源的LoadResources，LoadCharacter，然后是用户行为产生的事件，就是点击船，点击角色，然后是判断游戏输赢的check，最后是游戏输了可以restart。