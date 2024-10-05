# SikuThai
* ច្បាប់នៃការផ្គូផ្គងកាត៖
- សន្លឹកបៀដែលមានលេខ (A-9): គូត្រូវបានបង្កើតឡើងប្រសិនបើផលបូកនៃសន្លឹកបៀពីរស្មើ 10 ។ ឧទាហរណ៍៖
 => A + 9 = 10
 => 2 + 8 = 10
 => 3 + 7 = 10
 => 4 + 6 = 10
 => 5 + 5 = 10
- សន្លឹកបៀមុខ (10, J, Q, K)៖ សន្លឹកបៀទាំងនេះបង្កើតជាគូដែលមានចំណាត់ថ្នាក់ដូចគ្នា។ ឧទាហរណ៍៖
 => 10 គូជាមួយ 10
 => J គូជាមួយ J
 => Q គូជាមួយ Q
 => K គូជាមួយ K

* ការលេងហ្គេម៖
 #ចាប់ផ្តើមដៃ៖
 => អ្នកលេងចាប់ផ្តើមដោយ 8 សន្លឹក។
 => AI ចាប់ផ្តើមដោយ 7 សន្លឹក។
 #វេនអ្នកលេង៖
 => អ្នកលេងជ្រើសរើសកាតពីដៃរបស់ពួកគេដើម្បីទម្លាក់។
 => AI អាច "ស៊ី" កាតដែលបានទម្លាក់ប្រសិនបើវាបង្កើតជាគូជាមួយនឹងសន្លឹកបៀណាមួយរបស់វា។
 => ប្រសិនបើ AI មិនអាចផ្គូផ្គងកាតបានទេ វានឹងទាញសន្លឹកបៀចេញពីបាត។
 #វេនរបស់ AI៖
 => បន្ទាប់ពីគូររួច AI នឹងមាន 8 សន្លឹក។
 => AI នឹងជ្រើសរើសទម្លាក់កាតដែលមិនបង្កើតជាគូ។


* សកម្មភាពនិងប៊ូតុង៖
 => Eat Button: នេះអនុញ្ញាតឱ្យអ្នកលេង ឬ AI រើសសន្លឹកបៀដែលជ្រុះប្រសិនបើវាបង្កើតជាគូ។
 => Draw Button: ប្រើដើម្បីគូរសន្លឹកបៀរពីក្នុងទូក ប្រសិនបើគ្មានគូអាចបង្កើតបានជាមួយនឹងកាតដែលទម្លាក់។
 => Drop Button: អនុញ្ញាតឱ្យអ្នកលេង ឬ AI ទម្លាក់កាតមួយបន្ទាប់ពីញ៉ាំ ឬគូរសន្លឹកបៀ។ ជ្រើសរើសទម្លាក់សន្លឹកបៀដែលមិនបង្កើតជាគូ។

* លក្ខខណ្ឌឈ្នះ៖
 ហ្គេមបញ្ចប់នៅពេលដែលអ្នកលេង ឬ AI មានសន្លឹកបៀទាំងអស់នៅក្នុងដៃបង្កើតជាគូត្រឹមត្រូវ។

![iphone6 7](https://github.com/user-attachments/assets/726e322d-0dd8-483e-a5d5-d9c62e64669a)

* Card Pairing Rules:
- Numbered Cards (A-9): A pair is formed if the sum of two cards equals 10. For example:
   => A  + 9 = 10
   => 2 + 8 = 10
   => 3 + 7 = 10
   => 4 + 6 = 10
   => 5 + 5 = 10
- Face Cards (10, J, Q, K): These cards form pairs with the same rank. For example:
   => 10 pairs with 10
   => J pairs with J
   => Q pairs with Q
   => K pairs with K

* Gameplay:
  #Starting Hands:
     => The player starts with 8 cards.
     => The AI starts with 7 cards.
  #Player's Turn:
     => The player chooses a card from their hand to drop.
     => The AI can "Eat" the dropped card if it forms a pair with one of its cards.
     => If the AI cannot pair the card, it will draw a card from the deck.
  #AI's Turn:
     => After drawing, the AI will now have 8 cards.
     => The AI will choose to drop a card that does not form a pair.


* Actions and Buttons:
   => Eat Button: This allows the player or AI to pick up a dropped card if it forms a pair.
   => Draw Button: Used to draw a card from the deck if no pair can be formed with the dropped card.
   => Drop Button: Allows the player or AI to drop a card after eating or drawing a card.choose to drop a card that does not form a pair.

* Win Condition:
    The game ends when either the player or the AI     has all cards in hand forming valid pairs.
