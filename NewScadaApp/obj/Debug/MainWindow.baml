   M S B A M L   `   `   `  ����F  BNewScadaApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:NewScadaAppNewScadaApp  5   	       NewScadaApp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:NewScadaApp     5   	   -   5          � Title$  
MainWindow��5   	   $	��700��6   $
��1200��6)    � WindowState$ 	Maximized=�66    � 	Resources 5	         BackgroundGrid j� TileMode j� Viewport j� ViewportUnits L� Drawing � Geometry �� Rect
	 � Pen
 >� Brush >� 	Thickness � Brush �� Color��%!  &	        L� 5   
   $
 Tile=�5      $ -10,-10,40,40�6'   $ Absolute=�5       5      � 5       5      �� 5      $ 	0,0,50,50�6,   	 5      >� 5      

 �����$ 1Q�6+    5      �� 5      $ DarkSlateBlue��6*   5      5      5      .��5      �� #��[  6   .+�5   
   �� $��Simple SCADA��6    �� Left$ 30��6'   
 �� Top$ 10��68   
��� ���$��50(�6\   $��Arial Black)�6j   �� 5    
   -   5         ��Name$ 	LblStatus��5       $ 30��6#   $	 100��64   $��Sample��6E   
�������6V   �� 5!   
   -   5         ��Name$ BtnMonitoring��5!      $	 451��6(   $ 41��6:   $	��120��6J   $��30��6V   $��strat monitoring��6b   6}      NewScadaApp.UcTank  5$   
   $	��193��6   $	 300��6$   $	 321��66   $	��434��6G      NewScadaApp.UcPipe  5%   
   $	��100��6   $	 216��6$   $	 375��66   $	��100��6G     5&   
   $	��100��6   $	 720��6$   $	 375��66   $	��100��6G      NewScadaApp.UcMotor  5'   
   $	��162��6   $	 813��6%   $	 352��67   $	��180��6H      NewScadaApp.UcLED  5(   
   -   5          Name$ LedState��5(      $��78��6(   $	 578��64   $	 375��6F   $��72��6W      	CurrState$ #808080��6b      NewScadaApp.UcButton  5)   
   -   5          Name$ BtnMotor��5)         NewScadaApp.UcButton   CustomClick$ BtnMotor_CustomClick��6e   $��45��6+   $	 923��67   $	 401��6I   $��35��6Z   6e   5*      5+      