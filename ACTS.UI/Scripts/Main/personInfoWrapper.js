<script type="text/javascript">
            jQuery(document).ready(function($) {
                var $descriptionBlock = $('#description');

                $('.personWrapper').click(function(){
                    if(getDescription() == $(this).data('name')) {
                        hideDescription();
                    }
                    else 
                    {
                        hideDescription();
                        showDescription($(this));
                    }
                });

                /**
                 * Show description
                 */
                function showDescription($block) {
                    $block.addClass('active');
                    $descriptionBlock.detach().insertAfter($block.parent());
                    $descriptionBlock.data('id', $block.data('id'));

                    $('html, body').animate({
                        scrollTop: $block.offset().top
                    }, 500);

                    $(".personAvatar").attr('src', $block.data('img'));
                    $(".personName").text($block.data('name'));
                    $(".personDescription").html($block.find('.personDescription').html());
                    //$("#h_links").html($block.find('.person__links').html());
                    $descriptionBlock.show();
                }

                /**
                 * Hide description block
                 */
                function hideDescription() {
                    $('.row div').removeClass('active');
                    $descriptionBlock.hide();
                }

                /**
                 * Get description block ID
                 * @returns {*}
                 */
                function getDescription() {
                    if(!$descriptionBlock.is(':visible')) {
                        return false;
                    }
                    else {
                        return $descriptionBlock.data('name');
                    }
                }

            });
</script>