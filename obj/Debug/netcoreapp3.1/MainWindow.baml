   M S B A M L   `   `   `  ����G  CSpellChecker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:SpellCheckerSpellChecker  5   	       SpellChecker.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:SpellChecker     5   	     � Title$  
MainWindow��5   	   $	��600��6   $	��775��6)   .��5	      � .+�5
   
   ��  �� Orientation$ 
Horizontal=�6   .+�5   
   ]� $��10q�6   .+�5      � $��Center=�6   $	��Top=�65   $��0,0,0,10q�6J   .+�5      �� -   5        $��btnOpenFile��5      .��6F   	Open file6Q   5      �� 5      $ Vertical=�6   .+�5      �� -   5        $��
dictionary��5      $	��350��5      $	��200��5      $
��Auto=�5      $
��Auto=�5      �� 5      .��6   ===6   �� 5      -   5        $��textData��5      $	��350��5      $	��200��5      $
��Auto=�5      $
��Auto=�5      �� 5      .��6   ===6   5      5      ]� 5   
   $��10q�6   $��Right=�6    .+�5       � $��Center=�6   $	��Top=�65   $��0,0,0,10q�6J   .+�5!      �� -   5        $��Start��5!      .��6:   Start Correction6L   5"      �� 5#      $ Vertical=�6   .+�5$      �� -   5        $��output��5$       �� 
IsReadOnly$
 True��6(   $	��350��5%      $	��450��5&      $
��Auto=�5'      $
��Auto=�5(      5)      5*      5+      5,      5-      