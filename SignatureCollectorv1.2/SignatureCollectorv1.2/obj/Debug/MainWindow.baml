   M S B A M L   `   `   `  ����Q  MSignatureCollectorv1.2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:SignatureCollectorv1._2SignatureCollectorv1._2  5   	   (    "SignatureCollectorv1._2.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=3.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   1local%clr-namespace:SignatureCollectorv1._2  5   	     � Title$  
MainWindow��5	   	   $	��922��6   $
��1600��6)   .��5
      � ���	#FF329C1B$��Stretch=�6"   $��Stretch=�6@   .+�5   
   �� -   5         ��Name$ txtClientCode��5      $
��Left=�6)   $��23��6E   $��100,622,0,0q�6Q    �� TextWrapping$
 Wrap=�6f   $	��Top=�6z   $	��120��6�   -  'System.Windows.Input.KeyboardNavigation   TabIndex$ 0��6�   �� 5   
   -   5        $ txtStaffCode��5      $
��Left=�6)   $��23��6D   $��100,728,0,0q�6P   $
 Wrap=�6e   $	��Top=�6y   $	��120��6�   $ 1��6�   �� 5   
   -   5         ��Name$ label��5      $��Client Code:��6   $
��Left=�66   $��
21,614,0,0q�6Q   $	��Top=�6e   �� 5   
   -   5        $ 
label_Copy��5      $��Staff Code:��6$   $
��Left=�6:   $��
21,720,0,0q�6U   $	��Top=�6i   �� 5   
   -   5         ��Name$
 save��5      $
��Save��6   $
��Left=�6.   $��683,680,0,0q�6I   $	��Top=�6^   $��75��6v   $ 6��6�   �� 5   
   -   5        $
 sync��5      $
��Sync��6   $
��Left=�6.   $��683,744,0,0q�6I   $	��Top=�6^   $��75��6v   $ 7��6�   6�   �� 5   
   -   5        $ 	textClear��5      $��Clear��6$   $
��Left=�64   $��683,620,0,0q�6O   $	��Top=�6d   $��75��6|    J� RenderTransformOrigin$ 1.387,0.507&�6�   $ 5��6�   �� 5   
   -   5        $ 
StartLabel��5      $��Start Time:��6$   $
��Left=�6:   $��243,616,0,0q�6U   $	��Top=�6j   �� 5   
   -	   5        $ label2��5      $��	End Time:��6    $
��Left=�64   $��247,720,0,0q�6O   $	��Top=�6d   %  Xceed.Wpf.Toolkit.MaskedTextBox  5   
   -
   5          Name$ txtStartTime��5         Mask$ 00:00LL��63   $
��Left=�6B   $��23��6]   $��314,622,0,0q�6i   $
 Wrap=�6~   $	��Top=�6�   $	��120��6�   $ 2��6�     5   
   -   5        $ 
txtEndTime��5      $ 00:00LL��61   $
��Left=�6@   $��23��6[   $��314,728,0,0q�6g   $
 Wrap=�6|   $	��Top=�6�   $	��120��6�   $ 3��6�   �� 5   
   -   5        $ label1��5      $��Date:��6    $
��Left=�60   $��474,614,0,0q�6K   $	��Top=�6`     5   
   -   5        $ txtDate��5      $ 
00/00/0000��6.   $
��Left=�6@   $��23��6[   $��516,622,0,0q�6g   $
 Wrap=�6|   $	��Top=�6�   $	��120��6�   $ 4��6�   �� 5   
   -   5        $��spHorizontal��5      	 �� Orientation$	 
Horizontal=�6)   .+�5      �� -   5        
 ��Name$
 	Signature��5      $��Stretch=�6+   $	��560��6I   $��	10,10,0,0q�6V   $	��Top=�6i   $
��1544��6�    �� MoveEnabled .  6�   5      5      5      