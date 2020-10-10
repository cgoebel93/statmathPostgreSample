CREATE TABLE blog
(
 id serial PRIMARY KEY,
 title VARCHAR (50) NOT NULL,
 description VARCHAR (100) NOT NULL
);

ALTER TABLE “blog” OWNER TO postgres;

INSERT INTO public.blog	VALUES (1, 'Title1', 'Desc1');
INSERT INTO public.blog	VALUES (2, 'Title2', 'Desc2');
INSERT INTO public.blog	VALUES (3, 'Title3', 'Desc3');