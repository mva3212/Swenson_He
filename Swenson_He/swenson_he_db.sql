CREATE TABLE "attribute_types" (
"id" serial4 NOT NULL,
"name" text NOT NULL,
PRIMARY KEY ("id") 
)
WITHOUT OIDS;

CREATE TABLE "attributes" (
"id" serial4 NOT NULL,
"name" text NOT NULL,
"attribute_type_id" int4 NOT NULL,
PRIMARY KEY ("id") 
)
WITHOUT OIDS;

CREATE TABLE "products" (
"id" serial4 NOT NULL,
"name" text NOT NULL,
"product_type_id" int4 NOT NULL,
"sku" text NOT NULL,
PRIMARY KEY ("id") 
)
WITHOUT OIDS;

CREATE TABLE "product_types" (
"id" serial4 NOT NULL,
"name" text NOT NULL,
PRIMARY KEY ("id") 
)
WITHOUT OIDS;

CREATE TABLE "product_attributes" (
"id" serial4 NOT NULL,
"product_id" int4 NOT NULL,
"attribute_id" int4 NOT NULL,
PRIMARY KEY ("id") 
)
WITHOUT OIDS;


ALTER TABLE "products" ADD CONSTRAINT "product_product_type_fk" FOREIGN KEY ("product_type_id") REFERENCES "product_types" ("id");
ALTER TABLE "product_attributes" ADD CONSTRAINT "product_attributes_products_fk" FOREIGN KEY ("product_id") REFERENCES "products" ("id");
ALTER TABLE "product_attributes" ADD CONSTRAINT "product_attributes_attribute_fk" FOREIGN KEY () REFERENCES "attributes" ("id");
ALTER TABLE "attributes" ADD CONSTRAINT "attribute_attribute_type_fk" FOREIGN KEY ("attribute_type_id") REFERENCES "attribute_types" ("id");

