   M S B A M L   `   `   `  ����?  ;RNSR, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:RNSRRNSR  5          RNSR.AnItemControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $localclr-namespace:RNSR     5      $	��283��5   3   $��88.203��6?     � 	Resources  5         OnMouseEnter1 �� TargetProperty �� 
TargetName8  2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime �� Value  OnMouseLeave1  SelectorTemplate �� 
TargetType � 	Resources  Key �Name ��Name	 �� Triggers
 P� 
SourceName P� Property P� Value N� EnterActions  SelectorHover N� ExitActions ��Name  SelectorLeave��%{  &	        &	 �     &	 �    7  7  �� 5	   
   .��5
      �� $0 *(Panel.Background).(SolidColorBrush.Color)�6+   $ Selector��6r   .g�5        $ 0��6&   $ 	#00FFFF00��62     5      $ 0:0:0.1��6&   $ 	#20FFFF00��68   5      5      �� 5   
   .��5      �� $0 *(Panel.Background).(SolidColorBrush.Color)�6+   $ Selector��6r   .g�5        $ 0��6&   $ 	#20FFFF00��62     5      $ 0:0:0.1��6&   $ 	#00FFFF00��68   5      5      �� 5   
   " ��63    5      ���� 5      SelectorHover .��5      �� $* $(Shape.Fill).(SolidColorBrush.Color)�63   $ 
HoverColor��6t   .g�5        $ 0��6.   $ 	#00FFFF00��6:     5      $ 0:0:0.1��6.   $ 	#20FFFF00��6@   5      5      �� 5      SelectorLeave .��5      �� $* $(Shape.Fill).(SolidColorBrush.Color)�63   $ 
HoverColor��6t   .g�5        $ 0��6.   $ 	#20FFFF00��6:     5       $ 0:0:0.1��6.   $ 	#00FFFF00��6@   5!      5"      5#      .R�5$      � $
 grid��6   .+�5%      �� $ 
HoverColor��6   ���	#00FFFF00$��Stretch=�6A   $
��Auto��6_   $��Stretch=�6   ��6m   Z� 6   5&      	 5'      P� 5(      $

 grid��6>    �@J�IsMouseOver .  5)      �� 5*      8��  6*    5,      �� 5-      $# SelectorLeave_BeginStoryboard��6*   8�� 6Q   5/      51      52       � Triggers 53      :� 54   
    :� RoutedEvent$ UIElement.MouseEnter��6    :� 
SourceName$ Selector��6:   .[�55      �� #��[  6   56      :� 57   
   $ UIElement.MouseLeave��6   $ Selector��6:   .[�58      �� -   5        $# OnMouseLeave1_BeginStoryboard��58      #��[ 6E   59      .��5;      � -   5        $ AnItem��5;      $��0,0,0,4.982q�6   $
��Left=�60   .+�5<   
   �� -   5         ��Name$ ItemDescription��5<      $
��Left=�6+   $��60��6F    �� TextWrapping$
 Wrap=�6R   $��ItemDescription��6f   $	��Top=�6}   $	��223��6�   
�������$��0q�6�    � Cursor$ IBeam��6�   $��16(�6�   ��6�   Z� 6�   �� 5=   
   -   5        $ 	ItemPrice��5=      $
��Left=�6%   $��20��6@   $
 Wrap=�6L   $��	ItemPrice��6`   $	��Top=�6q   $��71.744��6�   $��0,60,0,0q�6�   $��0q�6�   ���	#FFD6D6D6$ IBeam��6�   $��16(�6�   ��6�   Z� 6�   �� 5>   
   -   5         ��Name$ 	ModButton��5>      $	��Mod��6$   $
��Left=�62   $	��Top=�6M   $��60��6e   $��	223,0,0,0q�6p   $��80��6�   $��0q�6�   ���	#FFF4982B$��24(�6�   $
��Bold"�6�   
���������6�   Z� 6  �� 5?   
   ���	#FF14AFDC$��71.744,60.16,60,3.203q�6%   �� 5@   
   -   5         ��Name$ SentItem��5@      $
��Left=�6&   $
��Wrap=�6A   $	��Top=�6U   $��112.214,60.018,0,0q�6m   $��20��6�   $��110.786��6�   $��16(�6�   
�������$��Sent To Kitchen��6�   �� 5A   
   -   5        $ Selector��5A      $��0,0,60,3.221q�6#   ���	#00FFFF00#���  6�   ��6P   Z� ��6g   Z� 6�   5B      5C      