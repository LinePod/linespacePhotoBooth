   M S B A M L   `   `   `  ���� S  OMicrosoft.Kinect.Toolkit, Version=1.8.0.0, Culture=neutral, PublicKeyToken=nulleHclr-namespace:Microsoft.Kinect.Toolkit;assembly=Microsoft.Kinect.ToolkitMicrosoft.Kinect.Toolkit   
PhotoBooth7   1Hpi.Hci.Bachelorproject1617.PhotoBooth.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     VkHclr-namespace:Microsoft.Kinect.Toolkit;assembly=Microsoft.Kinect.Toolkit     -     � Title$  Linepod Photo Booth�� � WindowStartupLocation$ CenterScreen=�$
��1324��$	��750�� � WindowState$ 	Maximized=� � 
ResizeMode$ NoResize=� � WindowStyle$
 None=� � 	Resources    MediumGreyBrush �� Color  KinectPurpleBrush  KinectBlueBrush �� 
TargetType �� Property	 �� Value  SnapshotButton
 �� 
TargetType �� Orientation ��Name �� TextAlignment ��Name �� Triggers P� Property P� Value �� 
TargetName  SquareCheckBox �Name��%�  &	        &	      &	 *     '�� ?     &	 t     7  7  &	 e    7  7  �� $ 	#ff6e6e6e���� $ 	#ff52318f���� $ 	#ff00BCF2���� " ��.���  �@J�SnapsToDevicePixels	 . �� " ��.���  � ��	 �� "
 ��.R�� .+��� $ 
Horizontal=�
������ .+��� $ SnapText��#��z��$
 Left=�$��Center=�8��  $��15(�� $��9,0,0,0q�.+��� $ 
SnapNormal��$��Images\SnapNormal.png��$
��None=�$��Center=��� $ 	SnapHover��$��Images\SnapHover.png��$
��None=�$��Center=�$y�	Collapsed=� P�  �@J�IsMouseOver . . ��� $ 
SnapNormal�� � y�$	 	Collapsed=��� $ 	SnapHover�� � y�$	 Visible=��� 8	  $ SnapText�� � ���� " ��.���  � ��	 �� "
 ��.R�� .+��� $ 
Horizontal=�
������ .+�� $ SquareCheckBoxChecked��.+��� $ CheckedNormal��$��Images\CheckedNormal.png��$
��None=�$��Center=��� $ CheckedHover��$��Images\CheckedHover.png��$
��None=�$��Center=�$y�	Collapsed=�� $ SquareCheckBoxUnchecked��$y�	Collapsed=�.+��� $ UncheckedNormal��$ ��Images\UncheckedNormal.png��$
��None=�$��Center=��� $ UncheckedHover��$��Images\UncheckedHover.png��$
��None=�$��Center=�$y�	Collapsed=��� $ SquareCheckBoxText��#��z��$
 Left=�$��Center=�8��  $��15(�$��9,0,0,0q� P�  �@d�	IsChecked$ falseY�. ��� $ SquareCheckBoxChecked�� � y�$	 	Collapsed=��� $ SquareCheckBoxUnchecked�� � y�$	 Visible=�P�  �@J�IsMouseOver . . ��� $ CheckedNormal�� � y�$	 	Collapsed=��� $ UncheckedNormal�� � y�$	 	Collapsed=��� $ CheckedHover�� � y�$	 Visible=��� $ UncheckedHover�� � y�$	 Visible=��� 8	  $ SquareCheckBoxText�� � ��.��� -   $��
layoutGrid��$��	10 0 10 0q� � ColumnDefinitions �� $��0* ��� $	��58* ��� $
��313* � � RowDefinitions �� $
��Auto ��� �� $��50 ��� $��0 ��� $��50 �.+�� $��0��$��0 0 0 20q�$��3�� �� $
��130* ��� $
��117* ��� $
��248* ��� $
��247* �.+��� $��0��$��Images\Logo.png��$
��Fill=�$��32��$��81��$
��Left=�$��0 10 0 5q��� $��3��$��0,0,-2,0q�$��Right=�$��Bottom=�#��[  $��Segoe UI)�$��18(�Linepod Photo Booth4   .Microsoft.Kinect.Toolkit.KinectSensorChooserUI  -   $��sensorChooserUi��$��2��$��Center=�$��104,0q�$	��Top=�� $��1��$��3�� �� $
��185* ��� $
��185* �.+��� -   $��Backdrop��$��Images\empty.jpg��$��0���� -   $��MaskedColor��$��0��(�  (� Stretch$ Uniform=�$��Center=�$��1��.���� -   $��Image��$	��640��$	��480���� -   $��buttonPrintSkel��$��2��#��[ $��Skeleton    ��$��32��$��Right=�$��Center=�$��
0,7,306,11q�$��2��$	��136���� -   $��buttonPrintOutlines��$��2��#��[ $��Outlines    ��$��32��$��Right=�$��Center=�$��
0,7,160,11q�$��2��$��130                 ���� -	   $��buttonPrintBoth��$��2��#��[ $��Print         ��$��32��$��Right=�$��Center=�$��	0,7,10,11q�$��2��$��130                 ��