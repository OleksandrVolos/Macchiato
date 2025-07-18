# 🍰 Macchiato Cafe — WPF-каса для міні-кафе

![Monk's Cafe Screenshot](https://kz.kursiv.media/wp-content/uploads/2025/02/58-20-2-1-1280x720.jpg)

Простий, швидкий і візуально приємний застосунок для роботи з замовленнями, розрахунками та збереженням чеків у форматі CSV. Розроблено з любовʼю до UX ❤️

---

## 🚀 Основні можливості

- ✅ Додавання позицій з валідацією (до 5 штук)
- 🗑 Видалення вибраної позиції
- 💰 Додавання чайових (відсотком або фіксованою сумою)
- 🧮 Автоматичний підрахунок Net, GST, Tip, Total
- 💾 Збереження замовлення у файл `.csv`
- 📂 Завантаження замовлення з файлу
- 🧹 Очищення всього списку
- 🚪 Завершення роботи з повідомленням
- 🎨 Анімації, підсвічування, ефекти кнопок для WOW-ефекту

---

## 🎞 UX-Фішки

- ✨ Анімація появи сум (`Total`, `Tip`, `Net`)
- ✅ Підсвітка полів при помилках
- 💡 Візуальний сплеск при додаванні позиції
- 🟢 Кнопки "вжимаються" при натисканні
- 🔔 Плавне повідомлення внизу замість MessageBox

---

## 🧠 Технології

- WPF (.NET 6 / 7 / 8)
- C#
- MV (частково), без зовнішніх бібліотек
- XAML + анімації через `Storyboard`
- CSV формат для збереження

---

## 🖼️ Інтерфейс

> Інтерфейс розділений на дві частини:
- Ліва панель — додавання/керування
- Права — таблиця з позиціями + автоматичні підсумки

---

## 📂 Структура проєкту
<pre> WpfApp1/ 
  ├── MainWindow.xaml # 🖼 UI 
  ├── MainWindow.xaml.cs # 💡 Логіка 
  ├── MenuItem.cs # 🍽 Модель позиції 
  ├── BillCalculator.cs # 🧮 Обчислення Net, Tip, GST 
  ├── README.md # 📖 Документація 
  ├── bin/ # ⚙️ Службові файли 
  └── obj/ # ⚙️ Службові файли
</pre>


---

## 📦 Можливі покращення

- 🖨 Підтримка друку та PDF
- 🌑 Темна тема
- 📊 Графіки продажів
- 👤 Авторизація персоналу
- 🖥 MAUI-версія під мобільні пристрої

---

## 👨‍💻 Автор

**Волосионок Олександр**  
Спеціальність: *Інженерія програмного забезпечення*  
❤️ Ukraine, 2025

---

## 📸 Скриншоти

![image](https://github.com/user-attachments/assets/e69a156f-e7ed-4198-81dc-72ff542365fa)

---

## ✅ Ліцензія

Безкоштовне використання в некомерційних цілях.  
Комерційне використання — за домовленістю 🙃

---

> **P.S.** Якщо вам сподобалося — ⭐️ проєкт на GitHub!

