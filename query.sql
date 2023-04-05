--предполагаю, что связь многие-ко-многим реализована через соединительную таблицу prod_cat_junction
SELECT product_name, category_name
FROM products AS p
LEFT OUTER JOIN prod_cat_junction AS pc ON pc.product_id = p.product_id
LEFT OUTER JOIN categories AS c ON c.category_id = pc.category_id
GROUP BY product_name, category_name
